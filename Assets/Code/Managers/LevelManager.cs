using System;
using System.Collections.Generic;
using Code.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private LevelDataLibrary _levelDataLibrary;
    [field: SerializeField] public List<FoodItem.FoodIdentity> AvailableFoodItems { get; private set; }

    private int _currentLevelIndex = 0;
    public int CurrentLevel => _currentLevelIndex;
    
    private void Start()
    {
        _currentLevelIndex = SaveLoadManager.Instance.LoadGameLevel();
    }

    public void NextLevelCounter() => _currentLevelIndex++;
    
    public LevelData GetLevelData() => _levelDataLibrary.LevelDataList[_currentLevelIndex];
    
    public FoodItem.FoodIdentity GetFoodItem() => AvailableFoodItems[Random.Range(0, AvailableFoodItems.Count)];

}
