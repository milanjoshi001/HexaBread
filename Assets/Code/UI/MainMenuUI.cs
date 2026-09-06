using Code.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : Singleton<MainMenuUI>
{
    [Header("Bottom Elements")]
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
    }
    
    private void OnEnable()
    {
        _levelText.SetText($"{SaveLoadManager.Instance.LoadGame() + 1}");
    }

    private void OnDestroy()
    {
        _playButton.onClick.RemoveListener(PlayButtonCallback);
    }
    
    private void PlayButtonCallback()
    {
        //GridManager.Instance.LoadGrid(LevelManager.Instance.LevelDataLibrary.LevelDataList[SaveLoadManager.Instance.LoadGame()].LevelGrid);
        GameplayUI.Instance.InitializeGame();
        gameObject.SetActive(false);
    }

    public void Activate(bool value) =>  gameObject.SetActive(value);
}
