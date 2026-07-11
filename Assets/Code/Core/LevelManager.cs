using System;
using Code.Utils;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private LevelDataLibrary _levelDataLibrary;

    public LevelDataLibrary LevelDataLibrary => _levelDataLibrary;

    public int CurrentLevel => _currentLevelIndex;

    private int _currentLevelIndex = 0;
    
    private void Start()
    {
        _currentLevelIndex = SaveLoadManager.Instance.LoadGame();
    }

    public void NextLevelCounter() => _currentLevelIndex++;


    public LevelData GetNextLevel() => _levelDataLibrary.LevelDataList[_currentLevelIndex];

    public LevelData GetSameLevel() =>
        _levelDataLibrary.LevelDataList[_currentLevelIndex];

}
