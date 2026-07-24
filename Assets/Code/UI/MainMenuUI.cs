using Code.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : Singleton<MainMenuUI>
{
    [Header("Top UI")]
    [SerializeField] TextMeshProUGUI _starsText;
    
    [Header("Elements")] 
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private Button _playButton;
    
    private void Start()
    {
        _playButton.onClick.AddListener(PlayButtonCallback);

        if (_backgroundImage.TryGetComponent(out GradientColor gradientColor))
        {
            _backgroundImage.SetVerticesDirty();
        }
        _starsText.SetText($"{StarsManager.Instance.Stars.TotalStars}");
    }

    private void OnEnable()
    {
        _levelText.SetText($"{SaveLoadManager.Instance.LoadGame() + 1}");
    }

    private void OnDestroy()
    {
        _playButton.onClick.RemoveListener(PlayButtonCallback);
    }

    public void RefreshStars() => _starsText.SetText($"{StarsManager.Instance.Stars.TotalStars}");
    
    public void Activate(bool value) => gameObject.SetActive(value);

    private void PlayButtonCallback()
    {
        //GridManager.Instance.LoadGrid(LevelManager.Instance.LevelDataLibrary.LevelDataList[SaveLoadManager.Instance.LoadGame()].LevelGrid);
        GameplayUI.Instance.InitializeGame();
        gameObject.SetActive(false);
    }
}
