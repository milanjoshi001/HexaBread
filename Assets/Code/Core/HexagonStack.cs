using System.Collections.Generic;
using UnityEngine;

public class HexagonStack : MonoBehaviour
{
    public List<Hexagon> Hexagons { get; private set; }

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
