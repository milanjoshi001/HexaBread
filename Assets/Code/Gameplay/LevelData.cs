using UnityEngine;

[CreateAssetMenu(fileName = "Level Data", menuName = "Level/Level Data")]
public class LevelData : ScriptableObject
{
    [SerializeField] private int _levelCompleteRequirement;
    
    public int LevelCompleteRequirement => _levelCompleteRequirement;
}
