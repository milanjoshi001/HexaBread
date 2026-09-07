using System;
using Code.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUI : Singleton<GameplayUI>
{

    [Header("Elements")] 
    [SerializeField] private TextMeshProUGUI _movesLeftText;
    [SerializeField] private GameObject _objectivePrefab;
    [SerializeField] private TextMeshProUGUI _levelText;

    private int _maxMoves;
    private LevelData _currentLevelData => LevelManager.Instance.GetLevelData();
    
    private void Start()
    {
        MergeManager.OnLastStackPlaced += CurrentLevelText;
        MergeManager.OnMoveChanged += UpdateRemainingMovesText;
    }

    private void OnDestroy()
    {
        MergeManager.OnLastStackPlaced -= CurrentLevelText;
        MergeManager.OnMoveChanged -= UpdateRemainingMovesText;
    }
    
    public void InitializeGame()
    {
        StackSpawner.Instance.GenerateStacks();
        MergeManager.Instance.InitializeLevel(_currentLevelData.MaxMoves);
        //TODO: refactor level requirements, currently it's taking 1 amount
        _maxMoves = _currentLevelData.MaxMoves;
        _movesLeftText.SetText($"{_maxMoves}");
        _levelText.SetText($"{LevelManager.Instance.CurrentLevel + 1}");
    }

    private void UpdateRemainingMovesText(int move) => _movesLeftText.SetText($"{move}");
    
    public void Activate(bool value) => gameObject.SetActive(value);

    private void CurrentLevelText()
    {
        //TODO: if grid filled or moves are out reset the level
    }
    
    public void NextLevelText()
    {
        //TODO: back to home
    }
}
