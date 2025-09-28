using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class GridGeneratorEditor : EditorWindow
{
    // UI Fields
    private Grid grid;
    private GameObject hexagon;
    private int size = 2;
    private int removeCount = 5;

    private GameObject generatedGrid;
    
    [MenuItem("Tools/Create base grid level")]
    public static void ShowWindow()
    {
        GetWindow<GridGeneratorEditor>("Prefab Creator");
    }
    
    private void OnGUI()
    {
        GUILayout.Label("Level Prefab Creator Setup", EditorStyles.boldLabel);
        
        GUILayout.Space(10);
        
        grid = (Grid) EditorGUILayout.ObjectField("Grid", grid, typeof(Grid), false);
        
        hexagon = (GameObject)EditorGUILayout.ObjectField("Hexagon", hexagon, typeof(GameObject), false);
        
        size = EditorGUILayout.IntField("Size", size);
        
        GUILayout.Space(10);
        
        // Grid Preview
        if (GUILayout.Button("Create Grid Preview"))
        {
            CreateGridPreview();
        }
        removeCount = EditorGUILayout.IntField("Hexagons to Remove", removeCount);
        if (GUILayout.Button("Randomize Hex Removal"))
        {
            if (generatedGrid != null)
                RandomizeHexes(generatedGrid, removeCount);
            else
                Debug.LogWarning("Please create the grid preview first.");
        }

        GUILayout.Space(10);
        if (GUILayout.Button("Save as Prefab"))
        {
            SavePrefab();
        }
        GUILayout.Space(10);
        if (GUILayout.Button("Delete Prefab"))
        {
            DestroyImmediate(generatedGrid);
        }
        GUILayout.Space(5);
        
        // Optional: Validation label
        if (grid == null || hexagon == null)
        {
            EditorGUILayout.HelpBox("Assign both objects for full setup functionality.", MessageType.Warning);
        }
    }

    private void CreateGridPreview()
    {
        // Basic validation
        if (grid == null || hexagon == null)
        {
            EditorUtility.DisplayDialog("Error", "Please assign both objects.", "OK");
            return;
        }
        if (generatedGrid != null) DestroyImmediate(generatedGrid);
        
        generatedGrid = new GameObject("GridLevel");
        generatedGrid.transform.position = Vector3.zero;
        
        GridGenerator setupScript = generatedGrid.AddComponent<GridGenerator>();
        setupScript.GenerateGrid(grid, hexagon, size);
        
        if (setupScript != null)
        {
            DestroyImmediate(setupScript);
        }
    }

    private void RandomizeHexes(GameObject gridParent, int hexRemoveCount)
    {
        List<Transform> hexes = new List<Transform>();
        
        foreach (Transform child in gridParent.transform)
            hexes.Add(child);
        
        HashSet<Transform> removed = new HashSet<Transform>();

        int attempts = 0;
        while (removed.Count < hexRemoveCount && attempts < 2000)
        {
            attempts++;
            Transform candidate = hexes[Random.Range(0, hexes.Count)];
            if (removed.Contains(candidate)) continue;

            candidate.gameObject.SetActive(false);

            if (IsValidConfiguration(hexes))
            {
                removed.Add(candidate);
            }
            else
            {
                candidate.gameObject.SetActive(true);
            }
        }

        foreach (var transform in removed.ToList())
        {
            removed.Remove(transform);
            DestroyImmediate(transform.gameObject);
        }

        Debug.Log($"Removed {removed.Count} hexagons.");
    }

    private bool IsValidConfiguration(List<Transform> hexes)
    {
        HashSet<Transform> visited = new HashSet<Transform>();
        Transform start = hexes.FirstOrDefault(h => h.gameObject.activeSelf);
        if (start == null) return false;

        Queue<Transform> q = new Queue<Transform>();
        q.Enqueue(start);
        visited.Add(start);

        while (q.Count > 0)
        {
            Transform cur = q.Dequeue();
            foreach (Transform neighbor in GetNeighbors(cur, hexes))
            {
                if (!visited.Contains(neighbor) && neighbor.gameObject.activeSelf)
                {
                    visited.Add(neighbor);
                    q.Enqueue(neighbor);
                }
            }
        }

        int activeCount = hexes.Count(h => h.gameObject.activeSelf);
        return visited.Count == activeCount;
    }

    private IEnumerable<Transform> GetNeighbors(Transform hex, List<Transform> allHexes)
    {
        float hexWidth = grid.CellToWorld(new Vector3Int(1, 0, 0)).magnitude;  // adjust to your prefab size
        foreach (Transform other in allHexes)
        {
            if (other == hex || !other.gameObject.activeSelf) continue;

            float dist = Vector3.Distance(hex.position, other.position);
            if (dist < hexWidth) // neighbor distance threshold
                yield return other;
        }
    }

    private void SavePrefab()
    {
        if (generatedGrid == null)
        {
            Debug.LogWarning("Generate a grid before saving.");
            return;
        }
        
        string path = EditorUtility.SaveFilePanelInProject(
            "Save Prefab", 
            "GridLevel", 
            "prefab", 
            "Please enter a location to save the prefab",
            "Assets/Art/Prefabs"
        );
        
        if (!string.IsNullOrEmpty(path))
        {
            PrefabUtility.SaveAsPrefabAsset(generatedGrid, path);
            
            DestroyImmediate(generatedGrid);
            
            Debug.Log($"Prefab saved to: {Path.GetFullPath(path)}");
            AssetDatabase.Refresh();
        }
        else
        {
            Debug.Log("Prefab creation canceled.");
        }
    }
}
