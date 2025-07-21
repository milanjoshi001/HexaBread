using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Level Data Library", menuName = "Level/Level Data Library")]
public class LevelDataLibrary : ScriptableObject
{
    [SerializeField] private List<LevelData> _levelDataList;
    
    public List<LevelData> LevelDataList => _levelDataList;
}
