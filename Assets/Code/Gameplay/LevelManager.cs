using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    
    [SerializeField] private LevelDataLibrary _levelDataLibrary;

    public LevelDataLibrary LevelDataLibrary => _levelDataLibrary;

    public int CurrentLevel => _currentLevelIndex;

    private int _currentLevelIndex = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        _currentLevelIndex = SaveLoadManager.Instance.LoadGame();
        LevelCompleteUI.OnLevelComplete += NextLevelCounter;
    }

    private void NextLevelCounter() => _currentLevelIndex++;


    public LevelData GetNextLevel() => _levelDataLibrary.LevelDataList[_currentLevelIndex];

    public LevelData GetSameLevel() =>
        _levelDataLibrary.LevelDataList[_currentLevelIndex];

}
