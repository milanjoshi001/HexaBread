using System;
using Code.Utils;
using UnityEngine;

public class LevelHandler : Singleton<LevelHandler>
{
    [SerializeField] private Transform _hideGameplay;

    private int _totalHexagons => MergeManager.Instance.TotalHexagonCollected;

    public void LoadLevel(GameObject gameObject)
    {
        MainMenuUI.Instance. Activate(false);
        _hideGameplay.gameObject.SetActive(false);

        var levelObject = Instantiate(gameObject, transform);
    }

    public int LevelProgression() => 0;

    public void CloseLevel()
    {
        _hideGameplay.gameObject.SetActive(true);
    }
}