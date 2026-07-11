using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class StackController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] LayerMask _hexagonLayerMask;
    [SerializeField] LayerMask _gridCellLayerMask;
    [SerializeField] LayerMask _groundCellLayerMask;
    
    [Header("Visuals")]
    [SerializeField] private Color _hoverColor;
    [SerializeField] private Color _resetGridCellColor;
    
    private HexagonStack _currentStack;
    private Vector3 _currentStackInitialPos;
    
    [Header("Data")]
    private GridCell _targetGridCell;
    private GridCell _prevCell;
    private GridCell _swapperCell;

    [Header("Actions")] 
    public static Action<GridCell> OnStackPlaced;

    private void Start()
    {
        if(InputManager.Instance.InputAction != null)
        {
            InputManager.Instance.InputAction.FindAction("Clicked").performed += ctx =>
            {
                if (!InputManager.Instance.InputAction.FindAction("Drag").WasPerformedThisFrame() &&
                    _currentStack == null)
                {
                    ManageClick(ctx);
                }
                
                if(PowerUpUI.Instance.IsStackDestroyerOn)
                    ManageStackDestroyer(ctx);
            };
            
            InputManager.Instance.InputAction.FindAction("Drag").performed += ctx =>
            {
                if(_currentStack != null && !InputManager.Instance.InputAction.FindAction("Clicked").WasPerformedThisFrame())
                {
                    ManageDrag(ctx);
                }
            };
            
            InputManager.Instance.InputAction.FindAction("Drop").performed += ctx =>
            {
                if(_currentStack != null)
                {
                    ManageDrop(ctx);
                }
            };
        }
    }

    private void ManageStackDestroyer(InputAction.CallbackContext ctx)
    {
        if(!PowerUpUI.Instance.IsStackDestroyerOn) return;
        
        if(!ctx.action.WasPerformedThisFrame()) return;

        RaycastHit hit;
        Physics.Raycast(GetClickedRay(), out hit, 500f, _hexagonLayerMask);
        
        if(hit.collider == null) return;
        
        HexagonStack stack = hit.collider.gameObject.GetComponent<Hexagon>().GetComponentInParent<HexagonStack>();
        if (stack == null)
            return;
        
        GameplayUI.Instance.TotalHexagonsRemoved(stack.Hexagons.Count);
        stack.StackDestroy();
        PowerUpUI.Instance.SetDestroyer(false);
        PowerUpUI.Instance.ConfirmationPanelActivation(false);
        StackSpawner.Instance.EnableStackParent();
    }

    private void ManageClick(InputAction.CallbackContext ctx)
    {
        if(PowerUpUI.Instance.IsStackDestroyerOn) return;
        if(!ctx.action.WasPerformedThisFrame()) return;
        RaycastHit hit;
        Physics.Raycast(GetClickedRay(), out hit,500f, _hexagonLayerMask);


        if (hit.collider == null) return;
        _prevCell = null;
        _currentStack = hit.collider.GetComponent<Hexagon>().HexStack;
        _currentStackInitialPos = _currentStack.transform.position;
        if(_currentStack != null && PowerUpUI.Instance.IsStackSwaperOn)
            _swapperCell = _currentStack.GetComponentInParent<GridCell>();
    }


    private void ManageDrag(InputAction.CallbackContext ctx)
    {
        if(PowerUpUI.Instance.IsStackDestroyerOn) return;
        if(!ctx.action.WasPerformedThisFrame()) return;

        RaycastHit hit;
        Physics.Raycast(GetClickedRay(), out hit,500f, _gridCellLayerMask);

        if (hit.collider == null)
            DraggingAboveGround();
        else
            DraggingAboveGridCell(hit);
    }


    private void DraggingAboveGround()
    {
        RaycastHit hit;
        Physics.Raycast(GetClickedRay(), out hit,500f, _groundCellLayerMask);

        if (hit.collider == null)
        {
            Debug.LogError("No ground detected!");
            return;
        }

        Vector3 currentStackTargetPosition = hit.point.With(y: 2);

        _currentStack.transform.position = Vector3.MoveTowards(_currentStack.transform.position,
            currentStackTargetPosition, Time.deltaTime * 30);

        GridManager.Instance.GridCells.ForEach(g => g.SetHexGridColor(_resetGridCellColor));

        _targetGridCell = null;
    }

    private void DraggingAboveGridCell(RaycastHit hit)
    {
        GridCell gridCell = hit.collider.GetComponent<GridCell>();
        
        if (PowerUpUI.Instance.IsStackSwaperOn)
        {
            if (!gridCell.IsOccupied)
            {
                DraggingAboveGround();
                return;
            }

            HighlightGridCell(gridCell);
            return;
        }

        if(gridCell.IsOccupied)
            DraggingAboveGround();
        else
            HighlightGridCell(gridCell);
    }
    
    private void SwapStacks()
    {
        if (_swapperCell == null || _targetGridCell == null)
            return;

        if (_swapperCell == _targetGridCell)
            return;

        if (!_swapperCell.IsOccupied || !_targetGridCell.IsOccupied)
            return;

        HexagonStack first = _swapperCell.Stack;
        HexagonStack second = _targetGridCell.Stack;

        // Swap references
        _swapperCell.AssignStack(second);
        _targetGridCell.AssignStack(first);

        first.Place();
        second.Place();

        OnStackPlaced?.Invoke(_swapperCell);
        OnStackPlaced?.Invoke(_targetGridCell);

        PowerUpUI.Instance.SetSwapper(false);
        PowerUpUI.Instance.ConfirmationPanelActivation(false);

        _swapperCell.SetHexGridColor(_resetGridCellColor);
        _targetGridCell.SetHexGridColor(_resetGridCellColor);

        _swapperCell = null;
        _targetGridCell = null;
        _currentStack = null;
    }

    private void HighlightGridCell(GridCell gridCell)
    {
        Vector3 currentStackTargetPosition = gridCell.transform.position.With(y: 2);

        _currentStack.transform.position = Vector3.MoveTowards(_currentStack.transform.position,
            currentStackTargetPosition, Time.deltaTime * 30);
        _prevCell?.SetHexGridColor(_resetGridCellColor);
        gridCell.SetHexGridColor(_hoverColor);
        _prevCell = gridCell;
        _targetGridCell = gridCell;
    }

    private void ManageDrop(InputAction.CallbackContext ctx)
    {
        if(!ctx.action.WasPerformedThisFrame()) return;

        if (PowerUpUI.Instance.IsStackSwaperOn)
        {
            SwapStacks();
            return;
        }

        if (_targetGridCell == null)
        {
            _currentStack.transform.position = _currentStackInitialPos;
            _currentStack = null;
            return;
        }

        _currentStack.transform.position = _targetGridCell.transform.position.With(y: 0.2f);
        _currentStack.transform.SetParent(_targetGridCell.transform);
        _currentStack.Place();

        _targetGridCell.AssignStack(_currentStack);
        OnStackPlaced?.Invoke(_targetGridCell);
        _targetGridCell.SetHexGridColor(_resetGridCellColor);
        _targetGridCell = null;
        _currentStack = null;
    }

    private Ray GetClickedRay() => Camera.main.ScreenPointToRay(InputManager.Instance.InputAction.FindAction("Drag").ReadValue<Vector2>());
}
