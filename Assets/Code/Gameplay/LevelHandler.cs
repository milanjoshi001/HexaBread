using System;
using Code.Utils;
using UnityEngine;

public class LevelHandler : Singleton<LevelHandler>
{
    [SerializeField] private Transform _hideGameplay;

    private Camera _camera;
    
    public int TotalHexagons => MergeManager.Instance.TotalHexagonCollected;

    private CafeShopObjectManager _cafeShopObjectManager;

    private void Start()
    {
        _camera = Camera.main;
    }

    public void Activate(bool value) => _hideGameplay.gameObject.SetActive(value);

    public void LoadLevel(CafeShopData  cafeShopData)
    {
        MainMenuUI.Instance.Activate(false);
        GameplayUI.Instance.Activate(false);
        CafeShopUI.Instance.Activate(true);
        CameraManager.Instance.ToggleProjection();
        Activate(false);
        if (_cafeShopObjectManager != null && _cafeShopObjectManager != cafeShopData.CafePrefab)
        {
            _cafeShopObjectManager.Activate(true);
            return;
        }

        _cafeShopObjectManager = null;
        var levelObject = Instantiate(cafeShopData.CafePrefab, transform);
        _cafeShopObjectManager = levelObject;
    }

    public int LevelProgression() => 0;

    public void CloseLevel()
    {
        MainMenuUI.Instance.Activate(true);
        GameplayUI.Instance.Activate(true);
        CafeShopUI.Instance.Activate(false);
        CameraManager.Instance.ToggleProjection();
        _cafeShopObjectManager.Activate(false);
        Activate(true);
    }
}