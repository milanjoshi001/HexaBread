using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LevelComplete : MonoBehaviour
{
    public static LevelComplete Instance;
    private Canvas _canvas;
    
    [SerializeField] Button _nextLevelButton;
    
    public static Action OnLevelComplete;
    
    private LevelData _nextLevelData;
    
    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    private void Start()
    {
        _nextLevelButton.onClick.AddListener(NextLevel);
        
        if(TryGetComponent(out _canvas))
            _canvas.enabled = false;
    }

    private void OnDestroy()
    {
        _nextLevelButton.onClick.RemoveListener(NextLevel);
    }

    public void SetLevelComplete()
    {
        OnLevelComplete?.Invoke();
        SaveLoadManager.Instance.SaveGame(LevelManager.Instance.CurrentLevel);
        _canvas.enabled = true;
        GridManager.Instance.ResetGridList();
    }

    private void NextLevel()
    {
        GridManager.Instance.LoadGrid(LevelManager.Instance.GetNextLevel().LevelGrid);
        _canvas.enabled = false;
    }
}
