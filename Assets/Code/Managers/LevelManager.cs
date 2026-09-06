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
    
    public LevelData GetLevelData() => _levelDataLibrary.LevelDataList[_currentLevelIndex];

}
