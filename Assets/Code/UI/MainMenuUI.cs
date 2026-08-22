using System.Collections;
using Code.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : Singleton<MainMenuUI>
{
    [SerializeField] private Image _backgroundImage;
    
    [Header("Bottom Elements")]
    [SerializeField] private Button _homeButton;
    [SerializeField] private Image _homeIcon;
    [SerializeField] private TextMeshProUGUI _homeLableText;
    [SerializeField] private GameObject _homePanel;
    [SerializeField] private Button _shopButton;
    [SerializeField] private Image _shopIcon;
    [SerializeField] private TextMeshProUGUI _shopLableText;
    [SerializeField] private GameObject _shopPanel;
    [SerializeField] private Button _levelButton;
    [SerializeField] private Image _levelIcon;
    [SerializeField] private TextMeshProUGUI _levelLableText;
    [SerializeField] private GameObject _levelPanel;
    [SerializeField] private Color _selectedColor;
    [SerializeField] private Color _deselectedColor;
    
    [Header("Elements")] 
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private Button _playButton;
    
    private Button _currentButton;
    private GameObject _currentPanel;
    private TextMeshProUGUI _currentText;
    private Image _currentIcon;

    private void Start()
    {
        _playButton.onClick.AddListener(PlayButtonCallback);

        _backgroundImage.SetVerticesDirty();
        
        _homeButton.onClick.AddListener(HomeButtonCallback);
        _shopButton.onClick.AddListener(ShopButtonCallback);
        _levelButton.onClick.AddListener(LevelButtonCallback);

        LeanTween.delayedCall(0.05f, HomeButtonCallback);
    }
    
    private void OnEnable()
    {
        _levelText.SetText($"Level {SaveLoadManager.Instance.LoadGame() + 1}");
    }

    private void OnDestroy()
    {
        _playButton.onClick.RemoveListener(PlayButtonCallback);
        _homeButton.onClick.RemoveListener(HomeButtonCallback);
        _shopButton.onClick.RemoveListener(ShopButtonCallback);
        _levelButton.onClick.RemoveListener(LevelButtonCallback);
    }

    #region Callbacks

    private void HomeButtonCallback()
    {
        Display(ref _homeButton, ref _homePanel, ref _homeLableText, ref _homeIcon);
    }

    private void ShopButtonCallback()
    {
        Display(ref _shopButton, ref _shopPanel, ref _shopLableText, ref _shopIcon);
    }

    private void LevelButtonCallback()
    {
        Display(ref _levelButton, ref _levelPanel, ref _levelLableText, ref _levelIcon);
    }

    #endregion
    
    private void PlayButtonCallback()
    {
        //GridManager.Instance.LoadGrid(LevelManager.Instance.LevelDataLibrary.LevelDataList[SaveLoadManager.Instance.LoadGame()].LevelGrid);
        GameplayUI.Instance.InitializeGame();
        gameObject.SetActive(false);
    }

    public void Activate(bool value) =>  gameObject.SetActive(value);

    private void Display(ref Button button, ref GameObject panel, ref TextMeshProUGUI textObject, ref Image icon)
    {
        if (_currentButton != null && _currentPanel != null)
        {
            _currentButton.interactable = true;
            _currentPanel.SetActive(false);
            _currentText.gameObject.SetActive(false);
            _currentText.color = _deselectedColor;
            _currentIcon.color = _deselectedColor;
            AnimateText(ref _currentButton, true);
        }
        
        _currentButton = button;
        _currentPanel = panel;
        _currentText = textObject;
        _currentIcon = icon;
        
        _currentPanel.SetActive(true);
        _currentText.gameObject.SetActive(true);
        _currentButton.interactable = false;
        _currentText.color = _selectedColor;
        _currentIcon.color = _selectedColor;
        AnimateText(ref _currentButton, false);
    }

    private void AnimateText(ref Button button, bool originalPos)
    {
        var iconObject = button.transform.GetChild(0).gameObject;
        var posObject = button.transform.GetChild(1);
        var backObject = button.transform.GetChild(2);
        
        if(originalPos)
            LeanTween.move(iconObject, backObject, 0.25f);
        else
            LeanTween.move(iconObject, posObject, 0.25f);
        
    }
    
}
