using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class MergeManager : MonoBehaviour
{
    public static MergeManager Instance;
    [Header("Elements")]
    private List<GridCell> _updatedGridCells = new List<GridCell>();

    public static Action<int> OnStackComplete;

    private Coroutine _testCorutine;

    public static Action OnGridCellOccupied;

    private bool IsMoving;
    private bool IsRemoving;
    private bool IsCompleteMerge;
    
    
    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        
        Application.targetFrameRate = 60;
        StackController.OnStackPlaced += StackPlacedCallback;
    }

    private void OnDestroy()
    {
        StackController.OnStackPlaced -= StackPlacedCallback;
    }

    private void StackPlacedCallback(GridCell gridCell)
    {
        StartCoroutine(StackPlaced(gridCell));
    }

    private IEnumerator StackPlaced(GridCell gridCell)
    {
        IsMoving = false;
        IsRemoving = false;
        IsCompleteMerge = false;
        
        _updatedGridCells.Add(gridCell);
        while (_updatedGridCells.Count > 0)
            yield return CheckMerge(_updatedGridCells[0]);
    }

    private IEnumerator CheckMerge(GridCell gridCell)
    {
        _updatedGridCells.Remove(gridCell);

        if (!gridCell.IsOccupied) yield break;

        List<GridCell> neighborGridCells = GetNeighborGridCells(gridCell);

        if (neighborGridCells.Count <= 0) yield break;
        
        Color topHexagonColor = gridCell.Stack.GetTopHexColor();

        List<GridCell> similarNeighborGridCells = GetSimilarNeighborGridCells(topHexagonColor, neighborGridCells);
        
        if (similarNeighborGridCells.Count <= 0) yield break;

        _updatedGridCells.AddRange(similarNeighborGridCells);
        
        List<Hexagon> hexagonsToAdd = GetHexagons(topHexagonColor, similarNeighborGridCells);

        RemoveHexagon(hexagonsToAdd, similarNeighborGridCells);

        MoveHexagons(gridCell, hexagonsToAdd);

        yield return new WaitForSeconds(0.2f + (hexagonsToAdd.Count + 1) * 0.025f);
        
        yield return CheckForCompleteStack(gridCell, topHexagonColor);


        yield return WaitForMerge();
    }

    #region Getting list of Hexagons

    private List<GridCell> GetNeighborGridCells(GridCell gridCell)
    {
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
        
        return neighborGridCells;
    }

    private List<GridCell> GetSimilarNeighborGridCells(Color topHexagonColor, List<GridCell> neighborGridCells)
    {

        List<GridCell> similarNeighborGridCells = new List<GridCell>();
        foreach (var neighborGridCell in neighborGridCells)
        {
            Color neighborGridCellColor = neighborGridCell.Stack.GetTopHexColor();

            if (topHexagonColor == neighborGridCellColor)
                similarNeighborGridCells.Add(neighborGridCell);
        }

        return similarNeighborGridCells;
    }

    private List<Hexagon> GetHexagons(Color topHexagonColor, List<GridCell> similarNeighborGridCells)
    {
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
        
        return hexagonsToAdd;
    }

    #endregion

    private void RemoveHexagon(List<Hexagon> hexagonsToAdd, List<GridCell> similarNeighborGridCells)
    {
        foreach (var neighborGridCell in similarNeighborGridCells)
        {
            HexagonStack neighborCellHexagonStack = neighborGridCell.Stack;
            
            foreach (var hexagon in hexagonsToAdd)
            {
                if (neighborCellHexagonStack.Contains(hexagon))
                    neighborCellHexagonStack.Remove(hexagon);
            }
        }
        IsRemoving = true;
    }

    private void MoveHexagons(GridCell gridCell, List<Hexagon> hexagonsToAdd)
    {
        float initialY = gridCell.Stack.Hexagons.Count * 0.2f;

        for (int i = 0; i < hexagonsToAdd.Count; i++)
        {
            Hexagon hexagon = hexagonsToAdd[i];

            float targetY = initialY + i * 0.2f;
            Vector3 targetLocalPos = Vector3.up * targetY;
            
            gridCell.Stack.AddHexagon(hexagon);
            hexagon.MoveToLocal(targetLocalPos);
        }
        IsMoving = true;
    }

    private IEnumerator CheckForCompleteStack(GridCell gridCell, Color topHexagonColor)
    {
        if(gridCell.Stack.Hexagons.Count < 10) yield break;

        List<Hexagon> similarHexagons = new List<Hexagon>();

        for (int i = gridCell.Stack.Hexagons.Count - 1; i >= 0; i--)
        {
            Hexagon hex = gridCell.Stack.Hexagons[i];
            
            if(hex.Color != topHexagonColor) break;
            
            similarHexagons.Add(hex);
        }

        int similarHexagonCount = similarHexagons.Count;
        
        if (similarHexagons.Count < 10) yield break;

        float delay = 0;

        while (similarHexagons.Count > 0)
        {
            similarHexagons[0].SetParent(null);
            similarHexagons[0].Vanish(delay);
            delay += 0.005f;
            
            gridCell.Stack.Remove(similarHexagons[0]);
            similarHexagons.RemoveAt(0);
        }
        
        OnStackComplete?.Invoke(similarHexagonCount);
        _updatedGridCells.Add(gridCell);
        
        yield return new WaitForSeconds(0.2f + (similarHexagonCount + 1) * 0.01f);
        
        IsCompleteMerge = true;
    }

    private IEnumerator WaitForMerge()
    {
        yield return new WaitForSeconds(0.2f + (GridManager.Instance.GetGridCells().Count + 1) * 0.025f);
        
        if (!IsMoving || !IsRemoving || !IsCompleteMerge) yield break;
        
        yield return new WaitUntil(() => GridManager.Instance.GetGridCells().All(g => g.IsOccupied));
        CheckForOccupiedGrids();
    }

    public bool CheckForOccupiedGrids()
    {
        if (GridManager.Instance.GetGridCells().All(g => g.IsOccupied)) return true;

        return false;
    }
}
