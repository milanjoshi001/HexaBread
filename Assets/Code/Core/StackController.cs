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

    private void ManageClick(InputAction.CallbackContext ctx)
    {
        if(!ctx.action.WasPerformedThisFrame()) return;
        RaycastHit hit;
        Physics.Raycast(GetClickedRay(), out hit,500f, _hexagonLayerMask);


        if (hit.collider == null) return;
        _prevCell = null;
        _currentStack = hit.collider.GetComponent<Hexagon>().HexStack;
        _currentStackInitialPos = _currentStack.transform.position;
    }


    private void ManageDrag(InputAction.CallbackContext ctx)
    {
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
        
        if(gridCell.IsOccupied)
            DraggingAboveGround();
        else
            NonOccupiedGridCell(gridCell);
    }

    private void NonOccupiedGridCell(GridCell gridCell)
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
