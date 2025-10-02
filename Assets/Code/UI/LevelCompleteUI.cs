using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompleteUI : MonoBehaviour
{
    public static LevelCompleteUI Instance;
    
    [SerializeField] private Canvas _canvas;
    [SerializeField] Button _nextLevelButton;
    
    public static Action OnLevelComplete;
    
    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    private void Start()
    {
        _nextLevelButton.onClick.AddListener(NextLevel);
        
        _canvas.enabled = false;
    }

    private void OnDestroy()
    {
        _nextLevelButton.onClick.RemoveListener(NextLevel);
    }

    public void SetLevelComplete()
    {
        InputManager.Instance.gameObject.SetActive(false);
        GridManager.Instance.ResetGridList();
        OnLevelComplete?.Invoke();
        SaveLoadManager.Instance.SaveGame(LevelManager.Instance.CurrentLevel);
        _canvas.enabled = true;
    }

    private void NextLevel()
    {
        InputManager.Instance.gameObject.SetActive(true);
        GameplayUI.Instance.NextLevelText();
        GridManager.Instance.LoadGrid(LevelManager.Instance.GetNextLevel().LevelGrid);
        _canvas.enabled = false;
    }
}
