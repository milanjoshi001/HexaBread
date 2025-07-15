using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MergeManager : MonoBehaviour
{
    private void Awake()
    {
        StackController.OnStackPlaced += StackPlacedCallback;
    }
    
    private void OnDestroy()
    {
        StackController.OnStackPlaced -= StackPlacedCallback;
    }

    private void StackPlacedCallback(GridCell gridCell)
    {
        //Does this cell has neighbors ?
        LayerMask gridCellMask = 1 << gridCell.gameObject.layer;

        List<GridCell> neighborGridCells = new List<GridCell>();
        
        Collider[] neighborGridCellColliders = Physics.OverlapSphere(gridCell.transform.position, 2, gridCellMask);

        foreach (var gridCellCollider in neighborGridCellColliders)
        {
            GridCell neighborGridCell = gridCellCollider.GetComponent<GridCell>();

            if (!neighborGridCell.IsOccupied) continue;

            if (neighborGridCell == gridCell) continue;
            
            neighborGridCells.Add(neighborGridCell);
        }

        if (neighborGridCells.Count < 0) return;
        
        Color topHexagonColor = gridCell.Stack.GetTopHexColor();

        List<GridCell> similarNeighborGridCells = new List<GridCell>();

        foreach (var neighborGridCell in neighborGridCells)
        {
            Color neighborGridCellColor = neighborGridCell.Stack.GetTopHexColor();

            if (neighborGridCellColor == topHexagonColor)
                similarNeighborGridCells.Add(neighborGridCell);
        }
        
        if (similarNeighborGridCells.Count < 0) return;
        
        List<Hexagon> hexagonsToAdd = new List<Hexagon>();

        foreach (var neighborGridCell in similarNeighborGridCells)
        {
            HexagonStack neighborCellHexagonStack = neighborGridCell.Stack;

            for (int i = neighborCellHexagonStack.Hexagons.Count-1; i >= 0; i--)
            {
                Hexagon hexagon = neighborCellHexagonStack.Hexagons[i];

                if (hexagon.Color != topHexagonColor)
                    break;
                
                hexagonsToAdd.Add(hexagon);

                hexagon.SetParent(null);
            }
        }

        foreach (var neighborGridCell in similarNeighborGridCells)
        {
            HexagonStack neighborCellHexagonStack = neighborGridCell.Stack;
            
            foreach (var hexagon in hexagonsToAdd)
            {
                if (neighborCellHexagonStack.Contains(hexagon))
                    neighborCellHexagonStack.Remove(hexagon);
            }
        }

        float initialY = gridCell.Stack.Hexagons.Count * 0.2f;

        for (int i = 0; i < hexagonsToAdd.Count; i++)
        {
            Hexagon hexagon = hexagonsToAdd[i];

            float targetY = initialY + i * 0.2f;
            Vector3 targetLocalPos = Vector3.up * targetY;
            
            gridCell.Stack.AddHexagon(hexagon);
            
            hexagon.transform.localPosition = targetLocalPos;
        }

    }

}
