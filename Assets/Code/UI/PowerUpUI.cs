using System;
using Code.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpUI : Singleton<PowerUpUI>
{
    [Header("Confirmation Panels")] 
    [SerializeField] private TextMeshProUGUI _confirmationPanelText;
    [SerializeField] private GameObject _confirmationPanel;
    [SerializeField] private GameObject _powerUpsPanel;
    [SerializeField] private Button _closeConfirmationButton;
    [SerializeField] private Button _acceptPowerUpButton;
    
    [Header("Buttons")]
    [SerializeField] private Button _regenerateStackButton;
    [SerializeField] private Button _removeStackButton;
    [SerializeField] private Button _swapStackButton;
    
    public bool IsStackDestroyerOn { get; private set; }
    public bool IsStackSwaperOn { get; private set; }
    
    public static Action OnStackRegenerate;
    public static Action OnStackCollapsed;
    public static Action OnPowerUpCanceled;
    public static Action OnSwapStack;
    
    private void Start()
    {
        _regenerateStackButton.onClick.AddListener(RegenerateStack);
        _removeStackButton.onClick.AddListener(RemoveStack);
        _acceptPowerUpButton.onClick.AddListener(AcceptStackRegeneration);
        _closeConfirmationButton.onClick.AddListener(PowerUpCanceled);
        _swapStackButton.onClick.AddListener(SwapStack);
    }


    private void OnDestroy()
    {
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
        StackSpawner.Instance.Activate(false);
        IsStackSwaperOn = true;
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
        StackSpawner.Instance.Activate(true);
        IsStackDestroyerOn = false;
        IsStackSwaperOn = false;
        StackSpawner.Instance.EnableStackParent();
        OnPowerUpCanceled?.Invoke();
    }
    
    public void ConfirmationPanelActivation(bool value)
    {
        _confirmationPanel.SetActive(value);
        _powerUpsPanel.SetActive(!value);
    }
    
    public void SetDestroyer(bool value) => IsStackDestroyerOn = value;
    public void SetSwapper(bool value) => IsStackSwaperOn = value;
}