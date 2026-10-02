using System;
using Code.Utils;

public class InventoryManager : Singleton<InventoryManager>
{
    public Inventory Inventory {get; private set;}

    private void Awake() => Inventory = new Inventory();
}