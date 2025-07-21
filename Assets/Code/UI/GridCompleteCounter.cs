using System;
using TMPro;
using UnityEngine;

public class GridCompleteCounter : MonoBehaviour
{
    public static GridCompleteCounter Instance;
    
    [SerializeField] private TextMeshProUGUI _gridCompletedCounterText;

    private int _targetAmount => LevelManager.Instance.GetLevelCompleteRequirement();

    private int _levelReq = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        MergeManager.OnStackComplete += SetGridCompleteCounter;
        _levelReq = _targetAmount;
        _gridCompletedCounterText.SetText($"{_levelReq}");
    }

    private void OnDestroy()
    {
        MergeManager.OnStackComplete -= SetGridCompleteCounter;
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
