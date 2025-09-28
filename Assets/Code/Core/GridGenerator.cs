using UnityEngine;
using UnityEngine.Serialization;

#if UNITY_EDITOR
using UnityEditor;

public class GridGenerator : MonoBehaviour
{
    public void GenerateGrid(Grid grid, GameObject hexagon, int size)
    {
        int counter = 1;
        transform.Clear();

        for (int x = -size; x <= size; x++)
        {
            for (int y = -size; y <= size; y++)
            {
                Vector3 spawnPos = grid.CellToWorld(new Vector3Int(x, y, 0));

                if (spawnPos.magnitude > grid.CellToWorld(new Vector3Int(1, 0, 0)).magnitude * size) continue;
                
                var hex = (GameObject)PrefabUtility.InstantiatePrefab(hexagon);
                var counterValue = counter <= 9 ? $"0{counter}" : $"{counter}";
                hex.name = $"{hexagon.name + counterValue}";
                hex.transform.position = spawnPos;
                hex.transform.rotation = Quaternion.identity;
                hex.transform.parent = transform;
                counter++;
            }
        }
    }
}

#endif