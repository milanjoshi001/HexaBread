using Code.Utils;
using TMPro;
using UnityEngine;

public class CurrencyUI :  Singleton<CurrencyUI>
{
    [SerializeField] private TextMeshProUGUI _coinsText;
    
    private void Start()
    {
        UpdateCoinsText();
    }

    public void UpdateCoinsText() => _coinsText.SetText($"{CurrencyManager.Instance.GameCurrency.TotalCoins}");
}