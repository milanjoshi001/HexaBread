using UnityEngine;

[CreateAssetMenu(fileName = "Level Data", menuName = "Level/Level Data")]
public class LevelData : ScriptableObject
{
    [SerializeField] private int _levelCompleteRequirement;
    [SerializeField] private GameObject _levelGrid;

    public int LevelCompleteRequirement => _levelCompleteRequirement;
    public GameObject LevelGrid => _levelGrid;
}
