using System.Collections.Generic;
using Code.Utils;
using UnityEngine;

namespace Code.AI
{
    public class KitchenEquipments : Singleton<KitchenEquipments>
    {
        [field: SerializeField] public List<KitchenEquipment> Equipments { get; private set; }
    }
}