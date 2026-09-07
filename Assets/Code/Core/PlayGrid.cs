using System.Collections.Generic;
using Code.Utils;
using UnityEngine;

public class PlayGrid : Singleton<PlayGrid>
{
    public List<GridCell> GridCells => _gridCells;
    [SerializeField] private List<GridCell> _gridCells = new List<GridCell>();
    
    public void Activate(bool value) => gameObject.SetActive(value);

    public void ResetGrid()
    {
        foreach (var gridCell in _gridCells)
        {
            var foodItem = gridCell.GetComponentInChildren<FoodItem>();
            
            if (foodItem != null) 
                Destroy(foodItem.gameObject);
        }
    }
}
