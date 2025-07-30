using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] private LevelDataLibrary _levelDataLibrary;


    private int _currentLevelIndex = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        LevelComplete.OnLevelComplete += NextLevelCounter;
        GridManager.Instance.LoadGrid(_levelDataLibrary.LevelDataList[0].LevelGrid);
    }

    private void NextLevelCounter()
    {
        _currentLevelIndex++;
        
        GameplayUI.Instance.ResetLevelText();
    }


    public LevelData GetNextLevel() => _levelDataLibrary.LevelDataList[_currentLevelIndex];

    public LevelData GetSameLevel() =>
        _levelDataLibrary.LevelDataList[_currentLevelIndex == 0 ? _currentLevelIndex : _currentLevelIndex - 1];

}
