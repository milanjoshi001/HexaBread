using System;
using Code.Utils;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompleteUI : Singleton<LevelCompleteUI>
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] Button _nextLevelButton;
    
    public bool IsLevelCompleted { get; private set; }
    
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
        IsLevelCompleted = true;
        
        LevelManager.Instance.NextLevelCounter();
        InputManager.Instance.gameObject.SetActive(false);
        GridManager.Instance.ResetGridList();
        SaveLoadManager.Instance.SaveGame(LevelManager.Instance.CurrentLevel);
        _canvas.enabled = true;
    }

    private void NextLevel()
    {
        IsLevelCompleted = false;
        StackSpawner.Instance.ResetStacks();
        StackSpawner.Instance.GenerateStacks();
        GameplayUI.Instance.NextLevelText();
        InputManager.Instance.gameObject.SetActive(true);
        GridManager.Instance.LoadGrid(LevelManager.Instance.GetNextLevel().LevelGrid);
        _canvas.enabled = false;
    }
}
