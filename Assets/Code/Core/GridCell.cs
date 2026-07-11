using System;
using System.Collections.Generic;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] Hexagon _hexagonPrefab;

    [Header("Settings")] 
    [SerializeField] private List<Color> _heaxgonsColors;
    public HexagonStack Stack { get; private set; }
    
    public bool IsOccupied
    {
        get => Stack != null;
        private set { }
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
