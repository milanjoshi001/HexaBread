using UnityEngine;

namespace Code.AI
{
    public class KitchenEquipment : MonoBehaviour
    {
        [field: SerializeField] public FoodItem.FoodIdentity FoodIdentity { get; private set; }
    }
}