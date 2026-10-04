using System.Collections.Generic;
using System.Linq;
using Code.AI;
using UnityEngine;
using UnityEngine.AI;

public class ServerAI : BaseAI<ServerState>
{
    private CustomerAI _currentCustomer;
    private CustomerAI _previousCustomer;
    private List<CustomerAI> _customers = new ();

    private void Start()
    {
        _state = ServerState.Idle;
    }
    
    private void Update()
    {
        if(GameState.Instance.CurrentGameState != GameStateType.Cafe)
        {
            _agent.isStopped = true;
            return;
        }
        
        if(_agent.isStopped) _agent.isStopped = false;
        
        switch (_state)
        {
            case ServerState.Idle:
                FindCustomer();
                break;
            
            case ServerState.TakingOrder:
                TakingOrder();
                break;

            case ServerState.CheckingFoodExist:
                CheckInventory();
                break;
            
            case ServerState.GoingToInventory:
                CheckInventoryArrival();
                break;

            case ServerState.CollectingFood:
                CollectFood();
                break;

            case ServerState.DeliveringFood:
                CheckCustomerArrival();
                break;
        }
    }
    
    private void FindCustomer()
    {
        var customers = FindObjectsByType<CustomerAI>().Where(c => c.GetCustomerWaitingForFood()).ToList();
        var randomCustomer = Random.Range(0, customers.Count);
        
        if (customers.Count > 0)
        {
            _currentCustomer = customers[randomCustomer];
            if (_currentCustomer == _previousCustomer)
                _currentCustomer = customers[randomCustomer < customers.Count - 1 ? randomCustomer + 1 : customers.Count - 1];
        }
        
        if(_currentCustomer == null)
        {
            TryDeliverFood();
            return;
        }

        if (_currentCustomer == null)
        {
            if(_agent.isStopped) _agent.isStopped = false;
            _agent.SetDestination(PaymentCounter.Instance.OwnerPoint);
            return;
        }
        
        _previousCustomer = _currentCustomer;
        
        if(!_currentCustomer.IsSeated)
        {
            SetState(ServerState.Idle);
            return;
        }
        
        SetState(ServerState.TakingOrder);
    }

    private void TakingOrder()
    {
        if (_currentCustomer == null)
        {
            SetState(ServerState.Idle);
            return;
        }
        if(_agent.isStopped) _agent.isStopped = false;
        _agent.SetDestination(_currentCustomer.transform.position);
        
        if(Vector3.Distance(transform.position, _currentCustomer.transform.position) < 1f)
        {
            _agent.isStopped = true;
            _currentCustomer.WaitingToTakeOrder(true);
            _customers.Add(_currentCustomer);
            SetState(ServerState.CheckingFoodExist);
        }
    }

    private void CheckInventory()
    {
        if (_currentCustomer == null)
        {
            SetState(ServerState.Idle);
            return;
        }

        var order = _currentCustomer.CurrentOrder;
        var inventory = InventoryManager.Instance.Inventory.InventoryByFoodIdentity;
        
        if(inventory.ContainsKey(order.FoodIdentity) && inventory.ContainsValue(order.Quantity))
            SetState(ServerState.GoingToInventory);
        else
        {
            _agent.isStopped = true;
            _currentCustomer = null;
            SetState(ServerState.Idle);
        }
    }

    private void CheckInventoryArrival()
    {
        var equipment = KitchenEquipments.Instance.Equipments.First(k => k.FoodIdentity == _currentCustomer.CurrentOrder.FoodIdentity);
        _agent.isStopped = false;
        _agent.SetDestination(equipment.transform.position);
        if(Vector3.Distance(transform.position, equipment.transform.position) < 1.1f)
            SetState(ServerState.CollectingFood);
    }

    private void CollectFood()
    {
        if(_currentCustomer == null) return;
        
        InventoryManager.Instance.Inventory.RemoveFoodIdentity(_currentCustomer.CurrentOrder.FoodIdentity,  _currentCustomer.CurrentOrder.Quantity);
        InventoryUI.Instance.UpdateInventoryStorageValues();
        SetState(ServerState.DeliveringFood);
    }
    
    private void CheckCustomerArrival()
    {
        if (_currentCustomer == null) return;

        if (_currentCustomer.CurrentOrder == null) return;
        
        _agent.SetDestination(_currentCustomer.transform.position);

        if(Vector3.Distance(transform.position, _currentCustomer.transform.position) < 1f)
        {
            _currentCustomer.TryDeliverFood(_currentCustomer.CurrentOrder.FoodIdentity, _currentCustomer.CurrentOrder.Quantity);
            _customers.Remove(_currentCustomer);
            _currentCustomer = null;
            CheckAnotherOrder();
        }
    }
    
    private void CheckAnotherOrder()
    {
        if (_currentCustomer != null) return;
        
        SetState(ServerState.Idle);
    }

    private void TryDeliverFood()
    {
        for (int i = 0; i < _customers.Count; i++)
        {
            if (InventoryManager.Instance.Inventory.InventoryByFoodIdentity.ContainsKey(_customers[i].CurrentOrder.FoodIdentity))
            {
                _currentCustomer = _customers[i];
                SetState(ServerState.GoingToInventory);
            }
            else
            {
                if(_agent.isStopped) _agent.isStopped = false;
                _agent.SetDestination(PaymentCounter.Instance.OwnerPoint);
            }
        }
    }
}

[System.Serializable]
public enum ServerState
{
    Idle,
    TakingOrder,
    CheckingFoodExist,
    GoingToInventory,
    CollectingFood,
    DeliveringFood,
    CollectingPayment
}