using System.Collections.Generic;
using System.Linq;

public class Inventory
{
    public Dictionary<FoodItem.FoodIdentity, int> InventoryByFoodIdentity {get; private set; }  = new ();
    private int _currentInventoryStorageLimit;

    public void AddFoodIdentity(FoodItem.FoodIdentity foodIdentity, int quantity)
    {
        int currentTotal = InventoryByFoodIdentity.Values.Sum();

        if (currentTotal + quantity > _currentInventoryStorageLimit)
            return;

        InventoryByFoodIdentity.TryAdd(foodIdentity, 0);
        InventoryByFoodIdentity[foodIdentity] += quantity;
    }

    public void RemoveFoodIdentity(FoodItem.FoodIdentity foodIdentity, int quantity)
    {
        if (!InventoryByFoodIdentity.TryGetValue(foodIdentity, out int storedQuantity))
            return;

        storedQuantity -= quantity;

        if (storedQuantity <= 0)
            InventoryByFoodIdentity.Remove(foodIdentity);
        else
            InventoryByFoodIdentity[foodIdentity] = storedQuantity;
    }

    public (FoodItem.FoodIdentity, int)? GetRequiredItemsFromInventory(FoodItem.FoodIdentity foodIdentity, int quantity)
    {
        if (!InventoryByFoodIdentity.TryGetValue(foodIdentity, out int currentTotal))
            return null;

        if (currentTotal < quantity)
            return null;
        
        return (foodIdentity, quantity);
    }

    public void UpgradeInventoryStorageLimit(int newLimit) => _currentInventoryStorageLimit = newLimit;
}