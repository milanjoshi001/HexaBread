using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class StackController : MonoBehaviour
{
    [Header("Layers")]
    [SerializeField] private LayerMask _foodLayerMask;
    [SerializeField] private LayerMask _gridCellLayerMask;
    [SerializeField] private LayerMask _groundCellLayerMask;

    [Header("Visuals")]
    [SerializeField] private Color _hoverColor;
    [SerializeField] private Color _resetGridCellColor;

    [Header("Position")]
    [SerializeField] private float _dragHeight = 2f;
    [SerializeField] private float _placedHeight = 0.2f;

    private FoodItem _currentFood;

    private Vector3 _currentFoodInitialPosition;

    private GridCell _targetGridCell;
    private GridCell _previousCell;
    private GridCell _swapperCell;

    private InputAction _clickAction;
    private InputAction _dragAction;
    private InputAction _dropAction;
    
    public static Action<GridCell> OnFoodPlaced;
    public static Action<GridCell, GridCell> OnFoodSwapped;
    public static Action<GridCell> OnStackPlaced;

    private void Start()
    {
        if (InputManager.Instance == null || InputManager.Instance.InputAction == null)
        {
            Debug.LogError("StackController: InputManager not initialized.");
            return;
        }

        _clickAction = InputManager.Instance.InputAction.FindAction("Clicked");

        _dragAction = InputManager.Instance.InputAction.FindAction("Drag");

        _dropAction = InputManager.Instance.InputAction.FindAction("Drop");

        if (_clickAction != null)
            _clickAction.performed += OnClicked;

        if (_dragAction != null)
            _dragAction.performed += OnDragged;

        if (_dropAction != null)
            _dropAction.performed += OnDropped;
    }

    private void OnDestroy()
    {
        if (_clickAction != null)
            _clickAction.performed -= OnClicked;

        if (_dragAction != null)
            _dragAction.performed -= OnDragged;

        if (_dropAction != null)
            _dropAction.performed -= OnDropped;
    }
    
    private void OnClicked(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        if (IsDestroyerActive())
        {
            DestroyFood();
            return;
        }

        if (_currentFood != null)
            return;

        PickUpFood();
    }

    private void PickUpFood()
    {
        RaycastHit hit;

        if (!Physics.Raycast(GetPointerRay(), out hit, 500f, _foodLayerMask))
            return;

        FoodItem food = hit.collider.GetComponentInParent<FoodItem>();

        if (food == null)
            return;

        _currentFood = food;

        _currentFoodInitialPosition = food.transform.position;

        _targetGridCell = null;
        _previousCell = null;
        
        if (IsSwapperActive())
        {
            _swapperCell = food.GetComponentInParent<GridCell>();

            if (_swapperCell == null)
            {
                _currentFood = null;
                return;
            }
        }
        else
        {
            _swapperCell = null;
        }

        food.transform.SetParent(null);

        food.ActivateCollider(false);
    }

    private void OnDragged(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        if (_currentFood == null)
            return;

        if (IsDestroyerActive())
            return;

        DragFood();
    }

    private void DragFood()
    {
        RaycastHit hit;

        if (Physics.Raycast(GetPointerRay(), out hit, 500f, _gridCellLayerMask))
        {
            DraggingAboveGridCell(hit);
        }
        else
        {
            DraggingAboveGround();
        }
    }

    private void DraggingAboveGround()
    {
        RaycastHit hit;

        if (!Physics.Raycast(GetPointerRay(), out hit, 500f, _groundCellLayerMask))
            return;

        Vector3 targetPosition = hit.point.With(y: _dragHeight);

        MoveFood(targetPosition);

        ResetGridHighlights();

        _targetGridCell = null;
    }

    private void DraggingAboveGridCell(RaycastHit hit)
    {
        GridCell cell = hit.collider.GetComponent<GridCell>();

        if (cell == null)
            return;
        
        if (IsSwapperActive())
        {
            if (!cell.IsOccupied || cell == _swapperCell)
            {
                DraggingAboveGround();
                return;
            }

            HighlightGridCell(cell);
            return;
        }
        
        if (cell.IsOccupied)
        {
            DraggingAboveGround();
            return;
        }

        HighlightGridCell(cell);
    }

    private void HighlightGridCell(GridCell cell)
    {
        Vector3 targetPosition = cell.transform.position.With(y: _placedHeight);

        MoveFood(targetPosition);

        if (_previousCell != cell)
        {
            _previousCell?.SetHexGridColor(_resetGridCellColor);

            cell.SetHexGridColor(_hoverColor);

            _previousCell = cell;
        }

        _targetGridCell = cell;
    }

    private void MoveFood(Vector3 targetPosition)
    {
        if (_currentFood == null)
            return;

        _currentFood.transform.position = Vector3.MoveTowards(_currentFood.transform.position, targetPosition, Time.deltaTime * 30f);
    }
    
    private void OnDropped(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        if (_currentFood == null)
            return;

        if (IsSwapperActive())
        {
            SwapFood();
            return;
        }

        if (_targetGridCell == null)
        {
            ReturnFood();
            ResetController();
            return;
        }

        PlaceFood();
    }

    private void PlaceFood()
    {
        GridCell cell = _targetGridCell;

        _currentFood.transform.SetParent(cell.transform);
        _currentFood.transform.localPosition = Vector3.up * _placedHeight;
        _currentFood.transform.localRotation = Quaternion.identity;
        _currentFood.ActivateCollider(false);
        
        cell.AssignFoodToGridCell(_currentFood);
        cell.SetHexGridColor(_resetGridCellColor);
        
        OnFoodPlaced?.Invoke(cell);
        OnStackPlaced?.Invoke(cell);

        ResetController();
    }
    

    private void SwapFood()
    {
        if (_swapperCell == null || _targetGridCell == null)
        {
            ReturnFood();
            ResetController();
            return;
        }

        FoodItem first = _swapperCell.FoodItem;
        FoodItem second = _targetGridCell.FoodItem;

        if (first == null || second == null)
        {
            ReturnFood();
            ResetController();
            return;
        }

        GridCell firstCell = _swapperCell;
        GridCell secondCell = _targetGridCell;

        firstCell.AssignFoodToGridCell(second);
        secondCell.AssignFoodToGridCell(first);

        first.transform.SetParent(secondCell.transform);
        second.transform.SetParent(firstCell.transform);

        first.transform.localPosition = Vector3.up * _placedHeight;
        second.transform.localPosition = Vector3.up * _placedHeight;

        first.transform.localRotation = Quaternion.identity;
        second.transform.localRotation = Quaternion.identity;

        first.ActivateCollider(false);
        second.ActivateCollider(false);

        OnFoodSwapped?.Invoke(firstCell, secondCell);

        if (PowerUpUI.Instance != null)
        {
            PowerUpUI.Instance.SetSwapper(false);
            PowerUpUI.Instance.ConfirmationPanelActivation(false);
        }

        ResetGridHighlights();

        ResetController();
    }
    
    private void DestroyFood()
    {
        RaycastHit hit;

        if (!Physics.Raycast(GetPointerRay(), out hit, 500f, _foodLayerMask))
            return;

        FoodItem food = hit.collider.GetComponentInParent<FoodItem>();

        if (food == null)
            return;

        GridCell cell = food.GetComponentInParent<GridCell>();

        if (cell == null)
            return;

        food.ActivateCollider(false);
        cell.ClearFood();
        food.Vanish(1f);

        if (PowerUpUI.Instance != null)
        {
            PowerUpUI.Instance.SetDestroyer(false);

            PowerUpUI.Instance.ConfirmationPanelActivation(false);
        }

        if (StackSpawner.Instance != null)
            StackSpawner.Instance.EnableStackParent();
    }
    
    private void ReturnFood()
    {
        if (_currentFood == null)
            return;

        _currentFood.transform.SetParent(null);
        _currentFood.transform.position = _currentFoodInitialPosition;
        _currentFood.ActivateCollider(true);

        ResetGridHighlights();
    }
    
    private void ResetController()
    {
        _currentFood = null;
        _targetGridCell = null;
        _previousCell = null;
        _swapperCell = null;
    }

    private void ResetGridHighlights()
    {
        if (PlayGrid.Instance == null)
            return;

        foreach (GridCell cell in PlayGrid.Instance.GridCells)
        {
            if (cell != null)
                cell.SetHexGridColor(_resetGridCellColor);
        }
    }

    private bool IsSwapperActive()
    {
        return PowerUpUI.Instance != null && PowerUpUI.Instance.IsStackSwaperOn;
    }

    private bool IsDestroyerActive()
    {
        return PowerUpUI.Instance != null && PowerUpUI.Instance.IsStackDestroyerOn;
    }

    private Ray GetPointerRay()
    {
        Camera camera = Camera.main;

        if (camera == null)
            return default;

        Vector2 pointerPosition;
        if (Pointer.current != null)
            pointerPosition = Pointer.current.position.ReadValue();
        else
            pointerPosition = Vector2.zero;

        return camera.ScreenPointToRay(pointerPosition);
    }
}