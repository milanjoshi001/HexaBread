using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class GridCell : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] Hexagon _hexagonPrefab;

    [Header("Settings")] 
    [SerializeField] private List<Color> _heaxgonsColors;
    public HexagonStack Stack { get; private set; }

    private SplineAnimate _splineAnimate;
    
    public bool IsOccupied
    {
        get => Stack != null;
        private set { }
    }

    public void LoadSplineAnimate()
    {
        if (_splineAnimate == null)
        {
            _splineAnimate = gameObject.AddComponent<SplineAnimate>();
            _splineAnimate.Container = gameObject.GetComponentInParent<SplineContainer>();
            _splineAnimate.AnimationMethod = SplineAnimate.Method.Speed;
            _splineAnimate.MaxSpeed = LevelManager.Instance.LevelDataLibrary.LevelDataList[LevelManager.Instance.CurrentLevel].ConveyorBeltSpeed;
            _splineAnimate.Play();
        }
    }

    private void Start()
    {
        if (transform.childCount > 1)
        {
            Stack = transform.GetChild(1).GetComponent<HexagonStack>();
            Stack.Initialize();
        }
    }

    public void AssignStack(HexagonStack stack)
    {
        Stack = stack;

        if (stack == null)
            return;

        stack.transform.SetParent(transform);
        stack.transform.localPosition = Vector3.up * 0.2f;
    }

    public void SetHexGridColor(Color color) =>
        transform.GetComponentInChildren<MeshRenderer>().material.color = color;

    private void GenerateInitialHexagons()
    {
        while (transform.childCount > 1)
        {
            Transform t = transform.GetChild(1);
            t.SetParent(null);
            DestroyImmediate(t.gameObject);
        }
        Stack = new GameObject("Initial Stack").AddComponent<HexagonStack>();
        Stack.transform.SetParent(transform);
        
        Stack.transform.localPosition = Vector3.up * 0.2f;

        for (int i = 0; i < _heaxgonsColors.Count; i++)
        {
            Vector3 spawnPos = Stack.transform.TransformPoint(Vector3.up * i * 0.2f);
            
            Hexagon hexagonInstance = Instantiate(_hexagonPrefab, spawnPos, Quaternion.identity);
            
            hexagonInstance.Color = _heaxgonsColors[i];
            Stack.AddHexagon(hexagonInstance);
        }
    }
}
