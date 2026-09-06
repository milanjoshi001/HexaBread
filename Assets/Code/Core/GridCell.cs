using UnityEngine;

public class GridCell : MonoBehaviour
{
    
    [Header("Visuals")]
    [SerializeField] private MeshRenderer _gridRenderer;

    [Header("Food Placement")]
    [SerializeField] private float _foodHeight = 0.2f;

    private FoodItem _foodItem;
    public FoodItem FoodItem => _foodItem;

    public bool IsOccupied => FoodItem != null;
    
    public void AssignFoodToGridCell(FoodItem food)
    {
        if (food == null)
            return;
        
        food.transform.SetParent(transform);
        food.transform.localPosition =
            Vector3.up * _foodHeight;
        
        _foodItem = food;
    }

    public void ClearFood() => _foodItem = null;
    
    public void SetHexGridColor(Color color)
    {
        if (_gridRenderer == null)
            _gridRenderer =
                GetComponentInChildren<MeshRenderer>();

        if (_gridRenderer == null)
            return;

        _gridRenderer.material.color = color;
    }

    
    public void ResetFoodPosition()
    {
        if (_foodItem == null)
            return;

        _foodItem.transform.SetParent(transform);

        _foodItem.transform.localPosition =
            Vector3.up * _foodHeight;

        _foodItem.transform.localRotation =
            Quaternion.identity;
    }
}