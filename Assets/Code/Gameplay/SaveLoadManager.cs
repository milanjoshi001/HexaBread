using System;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void SaveGame(int level)
    {
        PlayerPrefs.SetInt("Level", level);
        Debug.Log($"Level saved at {level}");
    }

    public int LoadGame()
    {
        Debug.Log($"Loading level {PlayerPrefs.GetInt("Level")}");
        return PlayerPrefs.GetInt("Level");
    }
}
