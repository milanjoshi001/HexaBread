using Code.Utils;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : Singleton<GameOverUI>
{
    [SerializeField] private Canvas _canvas;    
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _homeButton;
    
    private void Start()
    {
        _restartButton.onClick.AddListener(RestartGame);
        _homeButton.onClick.AddListener(Home);
       
        _canvas.enabled = false;

        MergeManager.OnLastStackPlaced += LevelFailed;
    }
    
    private void OnDestroy()
    {
        _restartButton.onClick.RemoveListener(RestartGame);
        _homeButton.onClick.RemoveListener(Home);
        MergeManager.OnLastStackPlaced -= LevelFailed;
    }
    
    private void LevelFailed()
    {
        LifeManager.Instance.LifeGone();
        InputManager.Instance.gameObject.SetActive(false);
        _canvas.enabled = true;
        ConveyorBelt.Instance.ResetConveyorBelt();
        //GridManager.Instance.ResetGridList();
    }

    private void RestartGame()
    {
        InputManager.Instance.gameObject.SetActive(true);
        _canvas.enabled = false;
        //GridManager.Instance.LoadGrid(LevelManager.Instance.GetSameLevel().LevelGrid);
    }
    
    private void Home()
    {
        StackSpawner.Instance.ResetStacks();
        _canvas.enabled = false;
        MainMenuUI.Instance.Activate(true);
    }
}
