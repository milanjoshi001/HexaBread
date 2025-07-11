using UnityEditor;
using UnityEngine;

public class FolderOrganizer : EditorWindow
{
    [MenuItem("Leoplay/Folder Organizer")]
    public static void Organizer()
    {
        //Art folders
        FolderCreator("Art");
        FolderCreator("Animations","Art");
        FolderCreator("Prefabs","Art");
        FolderCreator("Shaders","Art");
        FolderCreator("UI","Art");
        FolderCreator("VFX","Art");
        //Code folders
        FolderCreator("Code");
        FolderCreator("Ads","Code");
        FolderCreator("Core","Code");
        FolderCreator("UI","Code");
        FolderCreator("Gameplay","Code");
        FolderCreator("Utils","Code");
        //Gameplay
        FolderCreator("DesignData");
        FolderCreator("ScriptableObjects");
    }

    private static void FolderCreator(string name, string location = null)
    {
        if(!string.IsNullOrEmpty(location) && !AssetDatabase.IsValidFolder($"Assets/{location}/{name}"))
            AssetDatabase.CreateFolder($"Assets/{location}", name);
        else if(string.IsNullOrEmpty(location) && !AssetDatabase.IsValidFolder($"Assets/{name}"))
            AssetDatabase.CreateFolder($"Assets", name);
    }
}
