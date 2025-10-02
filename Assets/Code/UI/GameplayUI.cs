using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUI : MonoBehaviour
{
    public static GameplayUI Instance;
    
    [Header("Elements")]
    [SerializeField] private TextMeshProUGUI _gridCompletedCounterText;
    [SerializeField] private Button _regenerateStackButton;
    [SerializeField] private Button _removeStackButton;

    private int _targetAmount;
    private int _levelReq = 0;

    public static Action OnStackRegenerate;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        MergeManager.OnStackComplete += SetGridCompleteCounter;
        LevelCompleteUI.OnLevelComplete += NextLevelText;
        MergeManager.OnLastStackPlaced += CurrentLevelText;
        
        _regenerateStackButton.onClick.AddListener(RegenerateStack);
        _removeStackButton.onClick.AddListener(RemoveStack);
    }


    private void OnDestroy()
    {
        MergeManager.OnStackComplete -= SetGridCompleteCounter;
        LevelCompleteUI.OnLevelComplete -= NextLevelText;
        MergeManager.OnLastStackPlaced -= CurrentLevelText;
        
        _regenerateStackButton.onClick.RemoveListener(RegenerateStack);
        _removeStackButton.onClick.RemoveListener(RemoveStack);
    }

    private void RegenerateStack() => OnStackRegenerate?.Invoke();

    private void RemoveStack()
    {
        
    }

    public void InitializeGame()
    {
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
    }
    
    private void SetGridCompleteCounter(int counter)
    {
        _levelReq -= counter;
        if (_levelReq <= 0)
        {
            LevelCompleteUI.Instance.SetLevelComplete();
            _gridCompletedCounterText.gameObject.SetActive(false);
            return;
        }
        
        _gridCompletedCounterText.SetText($"{_levelReq}");
    }
}
