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

        _levelText.SetText($"Level {LevelManager.Instance.CurrentLevelIndex + 1}");
    }

    private void OnDestroy()
    {
        _playButton.onClick.RemoveListener(PlayButtonCallback);
    }

    private void PlayButtonCallback()
    {
        gameObject.SetActive(false);
    }
}
