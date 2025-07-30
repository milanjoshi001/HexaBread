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
        _targetAmount = LevelManager.Instance.GetNextLevel().LevelCompleteRequirement;
        _levelReq = _targetAmount;
        _gridCompletedCounterText.SetText($"{_levelReq}");

        LevelComplete.OnLevelComplete += ResetLevelText;
        
        _regenerateStackButton.onClick.AddListener(RegenerateStack);
    }


    private void OnDestroy()
    {
        MergeManager.OnStackComplete -= SetGridCompleteCounter;
        LevelComplete.OnLevelComplete -= ResetLevelText;
        
        _regenerateStackButton.onClick.RemoveListener(RegenerateStack);
    }

    private void RegenerateStack() => OnStackRegenerate?.Invoke();

    public void ResetLevelText()
    {
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
            LevelComplete.Instance.SetLevelComplete();
            _gridCompletedCounterText.gameObject.SetActive(false);
            return;
        }
        
        _gridCompletedCounterText.SetText($"{_levelReq}");
    }
}
