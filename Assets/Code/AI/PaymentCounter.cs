using System;
using Code.Utils;
using UnityEngine;

public class PaymentCounter : Singleton<PaymentCounter>
{
    [SerializeField] private Transform _payPoint;
    [SerializeField] private Transform _ownerPoint;
    public Vector3 PayPoint => _payPoint.position;
    public Vector3 OwnerPoint => _ownerPoint.position;
    
    public void AddMoney(int amount) => CurrencyManager.Instance.GameCurrency.AddCoins(amount);
}