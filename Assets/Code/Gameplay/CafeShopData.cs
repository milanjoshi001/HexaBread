using UnityEngine;

[CreateAssetMenu(fileName = "Cafe Data", menuName = "Level/Cafe Data")]
public class CafeShopData : ScriptableObject
{
    [field: SerializeField] public string CafeName { get; private set; }
    [field: SerializeField] public Sprite CafeImage { get; private set; }
    [field: SerializeField] public GameObject CafePrefab { get; private set; }
}