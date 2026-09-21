using System;
using UnityEngine;

public class Seat : MonoBehaviour
{
    public bool IsOccupied { get; private set; }

    private Transform _sitPoint;

    public Transform SitPoint => _sitPoint;

    private void Start()
    {
        TryGetComponent(out _sitPoint);
    }

    public bool TryReserve()
    {
        if (IsOccupied)
            return false;

        IsOccupied = true;
        return true;
    }

    public void Release()
    {
        IsOccupied = false;
    }
}