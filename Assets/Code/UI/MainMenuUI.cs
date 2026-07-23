using System;
using Code.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : Singleton<MainMenuUI>
{
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
    }

    private void OnEnable()
    {
        _levelText.SetText($"Level {SaveLoadManager.Instance.LoadGame() + 1}");
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
}
