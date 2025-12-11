using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUI : MonoBehaviour
{
    public static GameplayUI Instance;
    
    [Header("Elements")]
    [SerializeField] private TextMeshProUGUI _gridCompletedCounterText;
    
    [Header("Confirmation Panels")]
    [SerializeField] GameObject _confirmationPanel;
    [SerializeField] GameObject _powerUpsPanel;
    
    [Header("Buttons")]
    [SerializeField] private Button _regenerateStackButton;
    [SerializeField] private Button _removeStackButton;
    [SerializeField] private Button _acceptPowerUpButton;
    [SerializeField] private Button _swapStackButton;
    [SerializeField] private Button _closeConfirmationButton;
    
    public bool IsStackDestroyerOn { get; private set; }

    private int _targetAmount;
    private int _levelReq = 0;

    public static Action OnStackRegenerate;
    public static Action OnStackCollapsed;
    public static Action OnPowerUpCanceled;
    public static Action OnSwapStack;

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
        _acceptPowerUpButton.onClick.AddListener(AcceptStackRegeneration);
        _closeConfirmationButton.onClick.AddListener(PowerUpCanceled);
        _swapStackButton.onClick.AddListener(SwapStack);
    }


    private void OnDestroy()
    {
        MergeManager.OnStackComplete -= SetGridCompleteCounter;
        LevelCompleteUI.OnLevelComplete -= NextLevelText;
        MergeManager.OnLastStackPlaced -= CurrentLevelText;
        
        _regenerateStackButton.onClick.RemoveListener(RegenerateStack);
        _removeStackButton.onClick.RemoveListener(RemoveStack);
        _acceptPowerUpButton.onClick.RemoveListener(AcceptStackRegeneration);
        _closeConfirmationButton.onClick.RemoveListener(PowerUpCanceled);
        _swapStackButton.onClick.RemoveListener(SwapStack);
    }

    private void RegenerateStack()
    {
        _acceptPowerUpButton.gameObject.SetActive(true);
        ConfirmationPanelActivation(true);
    }

    private void AcceptStackRegeneration()
    {
        ConfirmationPanelActivation(false);
        OnStackRegenerate?.Invoke();
    }

    private void SwapStack()
    {
        _acceptPowerUpButton.gameObject.SetActive(false);
        ConfirmationPanelActivation(true);
        OnSwapStack?.Invoke();
    }

    private void RemoveStack()
    {
        _acceptPowerUpButton.gameObject.SetActive(false);
        ConfirmationPanelActivation(true);
        IsStackDestroyerOn = true;
        OnStackCollapsed?.Invoke();
    }

    private void PowerUpCanceled()
    {
        ConfirmationPanelActivation(false);
        IsStackDestroyerOn = false;
        StackSpawner.Instance.EnableStackParent();
        OnPowerUpCanceled?.Invoke();
    }
    
    public void ConfirmationPanelActivation(bool value)
    {
        _confirmationPanel.SetActive(value);
        _powerUpsPanel.SetActive(!value);
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

    public void TotalHexagonsRemoved(int count)
    {
        _gridCompletedCounterText.SetText($"{_targetAmount -= count}");
    }
    
    public void SetDestroyer(bool value) => IsStackDestroyerOn = value;
    
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
