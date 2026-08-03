using System;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class ObjectFill : MonoBehaviour
{
    [SerializeField] private float _fillProgressionAmount;
    [SerializeField] private float _maxFillAmount;
    [SerializeField] private Animator _animator;
    
    public float FillPercentage => _fillPercentage;
    public float MaxFillPercentage => _maxFillAmount;
    
    private float _fillPercentage;
    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void Start()
    {
        UpdateMaterials();
    }

    public void Fill()
    {
        if (_fillPercentage >= 1) return;

        _fillPercentage += _fillProgressionAmount;

        UpdateMaterials();
        
        _animator.Play("Fill");
    }

    private void UpdateMaterials()
    {
        foreach (var material in _renderer.materials)
        {
            material.SetFloat("_FillAmount", _fillPercentage * _maxFillAmount);
        }
    }
}