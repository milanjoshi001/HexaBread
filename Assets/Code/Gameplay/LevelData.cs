using UnityEngine;

[CreateAssetMenu(fileName = "Level Data", menuName = "Level/Level Data")]
public class LevelData : ScriptableObject
{
    [field: SerializeField] public int LevelCompleteRequirement { get; private set; }
    [field: SerializeField] public float ConveyorBeltSpeed { get; private set; }
    [field: SerializeField] public float HexagonOffset { get; private set; }
}
