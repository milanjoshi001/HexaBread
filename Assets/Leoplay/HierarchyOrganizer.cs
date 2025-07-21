using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
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
#endif