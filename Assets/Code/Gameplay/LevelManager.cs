using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] private LevelDataLibrary _levelDataLibrary;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public int GetLevelCompleteRequirement() => _levelDataLibrary.LevelDataList[0].LevelCompleteRequirement;

}
