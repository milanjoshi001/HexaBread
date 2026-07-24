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
        ConveyorBelt.Instance.ResetConveyorBelt();
        SaveLoadManager.Instance.SaveGame(LevelManager.Instance.CurrentLevel);
        StarsManager.Instance.Stars.AddStars(1);
        _canvas.enabled = true;
    }

    private void NextLevel()
    {
        IsLevelCompleted = false;
        //StackSpawner.Instance.ResetStacks();
        //StackSpawner.Instance.GenerateStacks();
        //GameplayUI.Instance.NextLevelText();
        //InputManager.Instance.gameObject.SetActive(true);
        _canvas.enabled = false;
        MainMenuUI.Instance.Activate(true);
        MainMenuUI.Instance.RefreshStars();
    }
}
