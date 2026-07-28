using Code.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : Singleton<MainMenuUI>
{
    [SerializeField] private Image _backgroundImage;
    
    [Header("Bottom Elements")] 
    [SerializeField] private Button _homeButton;
    [SerializeField] private GameObject _homeLableText;
    [SerializeField] private GameObject _homePanel;
    [SerializeField] private Button _shopButton;
    [SerializeField] private GameObject _shopLableText;
    [SerializeField] private GameObject _shopPanel;
    [SerializeField] private Button _levelButton;
    [SerializeField] private GameObject _levelLableText;
    [SerializeField] private GameObject _levelPanel;
    
    [Header("Elements")] 
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private Button _playButton;
    
    private Button _currentButton;
    private GameObject _currentPanel;
    private GameObject _currentText;

    private void Start()
    {
        HomeButtonCallback();
        _playButton.onClick.AddListener(PlayButtonCallback);

        _backgroundImage.SetVerticesDirty();
        
        _homeButton.onClick.AddListener(HomeButtonCallback);
        _shopButton.onClick.AddListener(ShopButtonCallback);
        _levelButton.onClick.AddListener(LevelButtonCallback);
        
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
        Display(ref _homeButton, ref _homePanel, ref _homeLableText);
    }

    private void ShopButtonCallback()
    {
        Display(ref _shopButton, ref _shopPanel, ref _shopLableText);
    }

    private void LevelButtonCallback()
    {
        Display(ref _levelButton, ref _levelPanel, ref _levelLableText);
    }

    #endregion
    
    private void PlayButtonCallback()
    {
        //GridManager.Instance.LoadGrid(LevelManager.Instance.LevelDataLibrary.LevelDataList[SaveLoadManager.Instance.LoadGame()].LevelGrid);
        GameplayUI.Instance.InitializeGame();
        gameObject.SetActive(false);
    }

    public void Activate(bool value) =>  gameObject.SetActive(value);

    private void Display(ref Button button, ref GameObject panel, ref GameObject textObject)
    {
        if (_currentButton != null && _currentPanel != null)
        {
            _currentButton.interactable = true;
            _currentPanel.SetActive(false);
            _currentText.SetActive(false);
            AnimateText(ref _currentButton, true);
        }
        
        _currentButton = button;
        _currentPanel = panel;
        _currentText = textObject;
        
        _currentPanel.SetActive(true);
        _currentText.SetActive(true);
        _currentButton.interactable = false;
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
