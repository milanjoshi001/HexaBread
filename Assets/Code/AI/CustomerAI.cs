using UnityEngine;
using UnityEngine.AI;

public class CustomerAI : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    
    private CustomerState _state;
    private Seat _assignedSeat;
    private FoodOrder _order;
    
    public FoodOrder CurrentOrder => _order;
    public CustomerState CurrentState => _state;
    public bool IsSeated { get; private set; }

    private void Start()
    {
        SetState(CustomerState.FindingSeat);
    }
    
    private void Update()
    {
        switch (_state)
        {
            case CustomerState.FindingSeat:
                FindSeat();
                break;

            case CustomerState.Sitting:
                CheckIfSeated();
                break;
            
            case CustomerState.Ordering:
                CreateOrder();
                break;
            
            case CustomerState.Eating:
                Eat();
                break;

            case CustomerState.Paying:
                Pay();
                break;

            case CustomerState.Leaving:
                Leave();
                break;
        }
    }
    
    private void SetState(CustomerState newState)
    {
        _state = newState;
    }
    
    private void FindSeat()
    {
        Seat seat = CafeManager.Instance.GetEmptySeat();

        if (seat == null)
            return;

        _assignedSeat = seat;

        _agent.SetDestination(_assignedSeat.SitPoint.position);

        SetState(CustomerState.Sitting);
    }

    public CustomerAI GetCustomerWaitingForFood()
    {
        if (_assignedSeat == null || _state != CustomerState.WaitingToOrder) return null;
        return this;
    }
    
    private void CheckIfSeated()
    {
        if (_agent.pathPending)
            return;

        if (_agent.remainingDistance > _agent.stoppingDistance)
            return;

        _agent.isStopped = true;

        // Snap/rotate to sitting position if necessary
        transform.SetPositionAndRotation(
            _assignedSeat.SitPoint.position,
            _assignedSeat.SitPoint.rotation
        );

        IsSeated = true;

        SetState(CustomerState.WaitingToOrder);
    }

    public void WaitingToTakeOrder(bool takingOrder)
    {
        if(takingOrder)
            SetState(CustomerState.Ordering);
    }
    
    private void CreateOrder()
    {
        _order = new FoodOrder
        {
            FoodIdentity = LevelManager.Instance.GetFoodItem(), Quantity = Random.Range(1, 2),
            TotalPrice = Random.Range(1, 100)
        };

        Debug.LogError($"{name} ordered {_order.Quantity} {_order.FoodIdentity}"
        );

        SetState(CustomerState.WaitingForFood);
    }
    
    public bool TryDeliverFood(FoodItem.FoodIdentity foodIdentity, int quantity)
    {
        if (_order == null)
            return false;

        if (_order.FoodIdentity != foodIdentity)
            return false;

        if (quantity < _order.Quantity)
            return false;

        ReceiveFood();

        return true;
    }
    
    private void ReceiveFood()
    {
        Debug.Log($"{name} received food!");

        SetState(CustomerState.Eating);
    }
    
    private float eatingTimer;

    private void Eat()
    {
        eatingTimer += Time.deltaTime;

        if (eatingTimer >= 5f)
        {
            eatingTimer = 0f;
            SetState(CustomerState.Paying);
        }
    }
    
    private void Pay()
    {
        _agent.SetDestination(PaymentCounter.Instance.PayPoint);
        _agent.isStopped = false;

        if (Vector3.Distance(transform.position, PaymentCounter.Instance.PayPoint) < 1.2f)
        {
            PaymentCounter.Instance.AddMoney(_order.TotalPrice);
            _assignedSeat.Release();
            SetState(CustomerState.Leaving);
        }
    }
    
    private void Leave()
    {
        _agent.SetDestination(AIStartPoint.Instance.ExitPoint.position);

        if (Vector3.Distance(transform.position, AIStartPoint.Instance.ExitPoint.position) < 1.1f)
        {
            StartFromEntryAgain();
            //Destroy(gameObject);
        }
    }

    private void StartFromEntryAgain()
    {
        _agent.SetDestination(AIStartPoint.Instance.EntryPoint.position);
        SetState(CustomerState.FindingSeat);
    }
}

[System.Serializable]
public enum CustomerState
{
    Entering,
    FindingSeat,
    Sitting,
    WaitingToOrder,
    Ordering,
    WaitingForFood,
    Eating,
    Paying,
    Leaving
}

[System.Serializable]
public class FoodOrder
{
    public FoodItem.FoodIdentity FoodIdentity;
    public int Quantity;
    public int TotalPrice;
}