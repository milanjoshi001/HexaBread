using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    public static GameOverUI Instance;
    private Canvas _canvas;
    
    [SerializeField] Button _restartButton;
    
    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }
    
    private void Start()
    {
        _restartButton.onClick.AddListener(RestartGame);
        
        if(TryGetComponent(out _canvas))
            _canvas.enabled = false;
    }
    
    private void OnDestroy()
    {
        _restartButton.onClick.RemoveListener(RestartGame);
    }
    
    private void LevelFailed()
    {
        _canvas.enabled = true;
        GridManager.Instance.ResetGridList();
    }

    private void RestartGame()
    {
        _canvas.enabled = false;
        GridManager.Instance.LoadGrid(LevelManager.Instance.GetSameLevel().LevelGrid);
    }
}
