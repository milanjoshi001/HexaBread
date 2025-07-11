using UnityEditor;
using UnityEngine;

public class HierarchyOrganizer : EditorWindow
{
    [MenuItem("Leoplay/Hierarchy Organizer")]
    public static void Organizer()
    {
        ObjectCreator("--- Environment ---");
        ObjectCreator("");
        ObjectCreator("--- Gameplay ---");
        ObjectCreator("");
        ObjectCreator("--- UI ---");
        ObjectCreator("");
        ObjectCreator("--- Managers ---");
    }

    private static void ObjectCreator(string name)
    {
        GameObject emptyObject = new GameObject(name);
    }
}
