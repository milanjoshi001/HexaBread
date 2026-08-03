using System;
using Code.Utils;
using UnityEngine;
using UnityEngine.UI;

public class CafeShopSelectionUI : Singleton<CafeShopSelectionUI>
{
    [SerializeField] private CafeShopsLibrary _cafeShopsLibrary;
    [SerializeField] private EnterShopButtonUI _enterShopButton;
    [SerializeField] private Transform _cafeShopsContainer;

    private void Start()
    {
        foreach (var cafeShop in _cafeShopsLibrary.CafeShops)
        {
            var shop = Instantiate(_enterShopButton, _cafeShopsContainer);
            shop.SetLevelData(cafeShop.CafeName, cafeShop.CafeImage, () => LoadLevel(cafeShop));
        }
    }

    private void LoadLevel(CafeShopData  cafeShopData)
    {
        LevelHandler.Instance.LoadLevel(cafeShopData.CafePrefab);
    }
}