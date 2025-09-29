using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private Button _playButton;

    private void Start()
    {
        _playButton.onClick.AddListener(PlayButtonCallback);

        _levelText.SetText($"Level {SaveLoadManager.Instance.LoadGame()}");
    }

    private void OnDestroy()
    {
        _playButton.onClick.RemoveListener(PlayButtonCallback);
    }

    private void PlayButtonCallback()
    {
        GridManager.Instance.LoadGrid(LevelManager.Instance.LevelDataLibrary.LevelDataList[SaveLoadManager.Instance.LoadGame()].LevelGrid);
        GameplayUI.Instance.InitializeGame();
        gameObject.SetActive(false);
    }
}
