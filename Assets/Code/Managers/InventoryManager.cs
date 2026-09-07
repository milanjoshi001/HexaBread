using System;
using Code.Utils;

public class InventoryManager : Singleton<InventoryManager>
{
    public Inventory Inventory {get; private set;}

    private void Start()
    {
        Inventory = new Inventory();
        
        
        Inventory.UpgradeInventoryStorageLimit(50);
    }
}