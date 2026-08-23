using System;
using System.Collections.Generic;
using System.Linq;
using Code.Utils;
using UnityEngine;
using UnityEngine.Splines;

public class ConveyorBelt : Singleton<ConveyorBelt>
{
    public List<GridCell> GridCells => _gridCells;
    [SerializeField] private List<GridCell> _gridCells = new List<GridCell>();
    
    public int GetUnoccupiedGridCells() => _gridCells.Count(g => !g.IsOccupied);

    public void ResetConveyorBelt()
    {
        foreach (var gridCell in _gridCells)
        {
            var hexagonStack = gridCell.GetComponentInChildren<HexagonStack>();
            
            if (hexagonStack != null) 
                Destroy(hexagonStack.gameObject);
        }
    }
}
