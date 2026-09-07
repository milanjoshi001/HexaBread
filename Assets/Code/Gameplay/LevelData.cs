using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level Data", menuName = "Level/Level Data")]
public class LevelData : ScriptableObject
{
    [field: SerializeField] public List<CollectionObjective> CollectionObjectives{ get; private set; }
    [field: SerializeField] public int MaxMoves { get; private set; }
    [field: SerializeField] public int CoinsRewarded { get; private set; }
}

[System.Serializable]
public class CollectionObjective
{
    public FoodItem.FoodIdentity FoodIdentity;
    public int RequiredAmount;
    
    [HideInInspector]
    public int CollectedAmount;

    public bool IsCompleted => CollectedAmount >= RequiredAmount;

    public void Reset() => CollectedAmount = 0;
}
