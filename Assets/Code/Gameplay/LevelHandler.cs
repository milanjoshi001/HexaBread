using System;
using Code.Utils;
using UnityEngine;

public class LevelHandler : Singleton<LevelHandler>
{
    [SerializeField] private Transform _hideGameplay;

    public int TotalHexagons => MergeManager.Instance.TotalHexagonCollected;

    public void LoadLevel(GameObject gameObject)
    {
        MainMenuUI.Instance.Activate(false);
        GameplayUI.Instance.Activate(false);
        CafeShopUI.Instance.Activate(true);
        _hideGameplay.gameObject.SetActive(false);

        var levelObject = Instantiate(gameObject, transform);
    }

    public int LevelProgression() => 0;

    public void CloseLevel()
    {
        MainMenuUI.Instance.Activate(true);
        GameplayUI.Instance.Activate(true);
        CafeShopUI.Instance.Activate(false);
        _hideGameplay.gameObject.SetActive(true);
    }
}