using System;
using Code.Utils;
using TMPro;
using UnityEngine;

public class InventoryUI : Singleton<InventoryUI>
{
    [SerializeField] private TextMeshProUGUI _storageLimit;

    private void Start()
    {
        UpdateInventoryStorageValues();
    }

    public void UpdateInventoryStorageValues()
    {
        _storageLimit.SetText($"{InventoryManager.Instance.Inventory.TotalItemsStored} / {InventoryManager.Instance.Inventory.CurrentInventoryStorageLimit}");
    }
}
