using System;
using Code.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : Singleton<MainMenuUI>
{
    [Header("Top Elements")] 
    [SerializeField] private TextMeshProUGUI _coinsText;
    
    [Header("Elements")] 
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private Button _playButton;

    private void Start()
    {
        _playButton.onClick.AddListener(PlayButtonCallback);

        _backgroundImage.SetVerticesDirty();
        LoadCoins();
    }

    private void OnEnable()
    {
        _levelText.SetText($"{SaveLoadManager.Instance.LoadGame() + 1}");
    }

    private void OnDestroy()
    {
        _playButton.onClick.RemoveListener(PlayButtonCallback);
    }
    
    public void LoadCoins() => _coinsText.SetText($"{CoinsManager.Instance.Coins.TotalCoins}");

    private void PlayButtonCallback()
    {
        //GridManager.Instance.LoadGrid(LevelManager.Instance.LevelDataLibrary.LevelDataList[SaveLoadManager.Instance.LoadGame()].LevelGrid);
        GameplayUI.Instance.InitializeGame();
        gameObject.SetActive(false);
    }

    public void Activate(bool value) =>  gameObject.SetActive(value);
}
