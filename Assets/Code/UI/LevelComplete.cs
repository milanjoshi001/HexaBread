using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelComplete : MonoBehaviour
{
    public static LevelComplete Instance;
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

    public void SetLevelComplete()
    {
        _canvas.enabled = true;
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(0);
        _canvas.enabled = false;
    }
}
