using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelComplete : MonoBehaviour
{
    public static LevelComplete Instance;
    private Canvas _canvas;
    
    [SerializeField] Button _restartButton;
    
    public static Action OnLevelComplete;
    
    private LevelData _nextLevelData;
    
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

    public void SetLevelComplete()
    {
        _canvas.enabled = true;
    }

    private void RestartGame()
    {
        OnLevelComplete?.Invoke();
        GridManager.Instance.LoadGrid(LevelManager.Instance.GetNextLevel().LevelGrid);
        _canvas.enabled = false;
    }
}
