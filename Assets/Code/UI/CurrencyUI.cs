using System;
using Code.Utils;
using TMPro;
using UnityEngine;

public class CurrencyUI :  Singleton<CurrencyUI>
{
    [SerializeField] private TextMeshProUGUI _coinsText;
    [SerializeField] private TextMeshProUGUI _diamondsText;

    private void Start()
    {
        UpdateCoinsText();
        UpdateDiamondsText();
    }

    public void UpdateCoinsText() => _coinsText.SetText($"{CurrencyManager.Instance.GameCurrency.TotalCoins}");

    public void UpdateDiamondsText() => _diamondsText.SetText($"{CurrencyManager.Instance.GameCurrency.TotalDiamonds}");
}