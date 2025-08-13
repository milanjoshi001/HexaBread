using System.Collections.Generic;
using UnityEngine;

public class HexagonStack : MonoBehaviour
{
    public List<Hexagon> Hexagons { get; private set; }
    private Color _currentHexagonColor;

    private int _numOfSimilarHexagons = 0;

    public void Initialize()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            AddHexagon(transform.GetChild(i).GetComponent<Hexagon>());
        }
        Place();
    }

    public void AddHexagon(Hexagon hexagon)
    {
        if(Hexagons == null)
            Hexagons = new List<Hexagon>();
            
        Hexagons.Add(hexagon);
        hexagon.SetParent(transform);
    }

    private int TotalSimilarHexagons()
    {
        _numOfSimilarHexagons = 0;
        _currentHexagonColor = GetTopHexColor();
        
        for (int i = 0; i < Hexagons.Count; i++)
        {
            if (Hexagons[i].Color == _currentHexagonColor)
                _numOfSimilarHexagons++;
        }
        
        return _numOfSimilarHexagons;
    }

    public void SetTotalSimilarHexagons()
    {
        if (Hexagons.Count < 1) return;
        Hexagons[^1].EnableText(true);
        Hexagons[^1].SetSimilarHexagonCount(TotalSimilarHexagons());
    }

    public void Place()
    {
        foreach (var hexagon in Hexagons)
        {
            hexagon.DisableCollider();
        }
    }

    public Color GetTopHexColor() => Hexagons[^1].Color;

    public bool Contains(Hexagon hexagon) => Hexagons.Contains(hexagon);

    public void Remove(Hexagon hexagon)
    {
        Hexagons.Remove(hexagon);
        
        if(Hexagons.Count <= 0)
            DestroyImmediate(gameObject);
    }

}
