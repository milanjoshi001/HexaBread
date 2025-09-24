using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

public class GridGenerator : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Grid _grid;
    [SerializeField] private GameObject _hexagon;
    [SerializeField] private int _size;
    
    [ContextMenu("Generate")]
    private void GenerateGrid()
    {
        transform.Clear();

        for (int x = -_size; x <= _size; x++)
        {
            for (int y = -_size; y <= _size; y++)
            {
                Vector3 spawnPos = _grid.CellToWorld(new Vector3Int(x, y, 0));

                if (spawnPos.magnitude > _grid.CellToWorld(new Vector3Int(1, 0, 0)).magnitude * _size) continue;
                
                var hexagon = (GameObject)PrefabUtility.InstantiatePrefab(_hexagon);
                hexagon.transform.position = spawnPos;
                hexagon.transform.rotation = Quaternion.identity;
                hexagon.transform.parent = transform;
            }
        }
    }
}

#endif