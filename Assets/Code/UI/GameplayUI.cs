using System;
using Code.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUI : Singleton<GameplayUI>
{
    [Header("Elements")]
    [SerializeField] private TextMeshProUGUI _gridCompletedCounterText;
    [SerializeField] private Image _fillImage;

    public bool IsLevelComplete => _levelReq == 0;
    private int _targetAmount;
    private int _levelReq = 0;
    
    private void Start()
    {
        MergeManager.OnStackComplete += SetGridCompleteCounter;
        MergeManager.OnLastStackPlaced += CurrentLevelText;
    }


    private void OnDestroy()
    {
        MergeManager.OnStackComplete -= SetGridCompleteCounter;
        MergeManager.OnLastStackPlaced -= CurrentLevelText;
    }
    
    public void InitializeGame()
    {
        StackSpawner.Instance.GenerateStacks();
        _targetAmount = LevelManager.Instance.GetNextLevel().LevelCompleteRequirement;
        _levelReq = _targetAmount;
        _gridCompletedCounterText.SetText($"{_levelReq}");
    }

    private void CurrentLevelText()
    {
        _gridCompletedCounterText.SetText($"{LevelManager.Instance.GetSameLevel().LevelCompleteRequirement}");
        _gridCompletedCounterText.gameObject.SetActive(true);
    }
    
    public void NextLevelText()
    {
        _targetAmount = 0;
        _targetAmount = LevelManager.Instance.GetNextLevel().LevelCompleteRequirement;
        _levelReq = _targetAmount;
        _gridCompletedCounterText.SetText($"{_levelReq}");
        _gridCompletedCounterText.gameObject.SetActive(true);
        _fillImage.fillAmount = 0;
    }

    public void TotalHexagonsRemoved(int count)
    {
        UpdateProgress(count);
    }
    
    public void SetGridCompleteCounter(int counter)
    {
        UpdateProgress(counter);
    }
    
    private bool UpdateProgress(int removed)
    {
        _levelReq = Mathf.Max(_levelReq - removed, 0);

        _fillImage.fillAmount = (_targetAmount - _levelReq) / (float)_targetAmount;
        _gridCompletedCounterText.SetText($"{_levelReq}");

        if (_levelReq == 0)
        {
            LevelCompleteUI.Instance.SetLevelComplete();
            CoinsManager.Instance.Coins.AddCoins(LevelManager.Instance.LevelDataLibrary
                .LevelDataList[LevelManager.Instance.CurrentLevel].CoinsRewarded);
            MainMenuUI.Instance.LoadCoins();
            _gridCompletedCounterText.gameObject.SetActive(false);
            return true;
        }

        return false;
    }
}
