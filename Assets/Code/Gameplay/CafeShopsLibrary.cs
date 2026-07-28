using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cafe Data Library", menuName = "Level/Cafe Data Library")]
public class CafeShopsLibrary : ScriptableObject
{
    [field: SerializeField] public List<CafeShopData> CafeShops { get; private set; }
}