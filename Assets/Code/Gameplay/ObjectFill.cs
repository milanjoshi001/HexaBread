using UnityEngine;

public class ObjectFill : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private float _fillProgressionAmount;
    [SerializeField] private float _maxFillPercentage;
    [SerializeField] private int _maxPointsRequired = 30;
    [SerializeField] private Animator _animator;
    
    public bool IsFull => _fillPercentage >= 1f;
    
    private float _fillPercentage;
    private int _currentPoints;
    
    private void Start()
    {
        UpdateMaterials();
    }

    public void Fill()
    {
        if (MergeManager.Instance.TotalHexagonCollected <= 0)
            return;
        
        if (_currentPoints >= _maxPointsRequired)
            return;

        _currentPoints++;

        _fillPercentage = (float)_currentPoints / _maxPointsRequired;

        UpdateMaterials();
        
        _animator.Play("FillBump");
        MergeManager.Instance.RemoveHexagonFromCollected(1);
        
        if (_currentPoints >= _maxPointsRequired)
        {
            CafeShopObjectManager.Instance.CheckObjectFill();
        }
    }

    private void UpdateMaterials()
    {
        foreach (var material in _renderer.materials)
        {
            material.SetFloat("_Fill_Percent", _fillPercentage * _maxFillPercentage);
        }
    }
}