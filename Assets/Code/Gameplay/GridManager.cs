using System;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;
    public List<GridCell> GridCells => _gridCells;
    private List<GridCell> _gridCells = new List<GridCell>();
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void LoadGrid(GameObject gridObject)
    {
        transform.Clear();
        
        var grid = Instantiate(gridObject, transform.position, Quaternion.identity);
        
        grid.transform.SetParent(transform);

        for (int i = 0; i < grid.transform.childCount; i++)
        {
            _gridCells.Add(grid.transform.GetChild(i).GetComponent<GridCell>());
        }
    }

    public void ResetGridList() => _gridCells.Clear();

}
