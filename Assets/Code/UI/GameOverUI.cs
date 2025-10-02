using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    public static GameOverUI Instance;
    
    [SerializeField] private Canvas _canvas;    
    [SerializeField] Button _restartButton;
    
    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }
    
    private void Start()
    {
        _restartButton.onClick.AddListener(RestartGame);
       
        _canvas.enabled = false;

        MergeManager.OnLastStackPlaced += LevelFailed;
    }
    
    private void OnDestroy()
    {
        _restartButton.onClick.RemoveListener(RestartGame);
        MergeManager.OnLastStackPlaced -= LevelFailed;
    }
    
    private void LevelFailed()
    {
        LifeManager.Instance.LifeGone();
        InputManager.Instance.gameObject.SetActive(false);
        _canvas.enabled = true;
        GridManager.Instance.ResetGridList();
    }

    private void RestartGame()
    {
        InputManager.Instance.gameObject.SetActive(true);
        _canvas.enabled = false;
        GridManager.Instance.LoadGrid(LevelManager.Instance.GetSameLevel().LevelGrid);
    }
}
