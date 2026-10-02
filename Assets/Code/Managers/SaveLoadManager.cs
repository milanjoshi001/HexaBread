using System;
using Code.Utils;
using UnityEngine;

public class SaveLoadManager : Singleton<SaveLoadManager>
{
    public void SaveGameLevel(int level)
    {
        PlayerPrefs.SetInt("Level", level);
        Debug.Log($"Level saved at {level}");
    }

    public int LoadGameLevel()
    {
        Debug.Log($"Loading level {PlayerPrefs.GetInt("Level")}");
        return PlayerPrefs.GetInt("Level");
    }
}
