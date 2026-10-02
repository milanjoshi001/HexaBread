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
    
    private void Start()
    {
        _playButton.onClick.AddListener(PlayButtonCallback);
    }
    
    private void OnEnable()
    {
        _levelText.SetText($"{SaveLoadManager.Instance.LoadGameLevel() + 1}");
    }

    private void OnDestroy()
    {
        _playButton.onClick.RemoveListener(PlayButtonCallback);
    }
    
    private void PlayButtonCallback()
    {
        TransitionUI.Instance.StartTransition(() =>
        {
            GameplayUI.Instance.InitializeGame();
            CafeManager.Instance.Activate(false);
            gameObject.SetActive(false);
            GameState.Instance.SetState(GameStateType.Gameplay);
        });
    }

    public void Activate(bool value) =>  gameObject.SetActive(value);
}
