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
        
        _canvas.enabled = false;
    }

    private void OnDestroy()
    {
        _nextLevelButton.onClick.RemoveListener(NextLevel);
        _homeButton.onClick.RemoveListener(Home);
    }

    public void SetLevelComplete()
    {
        IsLevelCompleted = true;
        
        LevelManager.Instance.NextLevelCounter();
        InputManager.Instance.gameObject.SetActive(false);
        ConveyorBelt.Instance.ResetConveyorBelt();
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
        _canvas.enabled = false;
    }
    
    private void Home()
    {
        IsLevelCompleted = false;
        StackSpawner.Instance.ResetStacks();
        _canvas.enabled = false;
        MainMenuUI.Instance.Activate(true);
    }
}
