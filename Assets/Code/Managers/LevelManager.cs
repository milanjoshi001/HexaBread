using System;
using System.Collections.Generic;
using Code.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private LevelDataLibrary _levelDataLibrary;

    public LevelDataLibrary LevelDataLibrary => _levelDataLibrary;
    
    [field: SerializeField] public List<FoodItem.FoodIdentity> AvailableFoodItems { get; private set; }

    public int CurrentLevel => _currentLevelIndex;

    private int _currentLevelIndex = 0;
    
    private void Start()
    {
        _currentLevelIndex = SaveLoadManager.Instance.LoadGame();
    }

    public void NextLevelCounter() => _currentLevelIndex++;
    
    public LevelData GetLevelData() => _levelDataLibrary.LevelDataList[_currentLevelIndex];
    
    public FoodItem.FoodIdentity GetFoodItem() => AvailableFoodItems[Random.Range(0, AvailableFoodItems.Count)];

}
