using System;
using Code.Utils;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompleteUI : Singleton<LevelCompleteUI>
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private Button _homeButton;
    
    public bool IsLevelCompleted { get; private set; }
    
    private void Start()
    {
        _nextLevelButton.onClick.AddListener(NextLevel);
        _homeButton.onClick.AddListener(Home);
        MergeManager.OnLevelComplete += SetLevelComplete;
        
        _canvas.enabled = false;
    }

    private void OnDestroy()
    {
        _nextLevelButton.onClick.RemoveListener(NextLevel);
        _homeButton.onClick.RemoveListener(Home);
        MergeManager.OnLevelComplete -= SetLevelComplete;
    }

    public void SetLevelComplete()
    {
        IsLevelCompleted = true;
        
        LevelManager.Instance.NextLevelCounter();
        InputManager.Instance.gameObject.SetActive(false);
        PlayGrid.Instance.ResetGrid();
        SaveLoadManager.Instance.SaveGameLevel(LevelManager.Instance.CurrentLevel);
        _canvas.enabled = true;
    }

    private void NextLevel()
    {
        IsLevelCompleted = false;
        ItemSpawner.Instance.ResetStacks();
        ItemSpawner.Instance.GenerateStacks();
        GameplayUI.Instance.NextLevelText();
        InputManager.Instance.gameObject.SetActive(true);
        _canvas.enabled = false;
        InventoryUI.Instance.UpdateInventoryStorageValues();
    }
    
    private void Home()
    {
        IsLevelCompleted = false;
        ItemSpawner.Instance.ResetStacks();
        _canvas.enabled = false;
        PlayGrid.Instance.Activate(false);
        GameplayUI.Instance.Activate(false);
        MainMenuUI.Instance.Activate(true);
        GameState.Instance.SetState(GameStateType.Cafe);
        InventoryUI.Instance.UpdateInventoryStorageValues();
    }
}
