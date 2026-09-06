using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue Data", menuName = "Dialogue/Dialogue Data")]
public class DialogueData : ScriptableObject
{
    [field: SerializeField] public List<DialogueContentData> DialogueContentDatas { get; private set; }
}

[System.Serializable]
public class DialogueContentData
{
    [field: SerializeField] public Sprite CharacterSprite { get; private set; }
    [field: SerializeField] public string CharacterName { get; private set; }
    [field: SerializeField] public string DialogueLine { get; private set; }
    [field: SerializeField] public DialoguePosition DialoguePosition { get; private set; }
    
}

[System.Serializable]
public enum DialoguePosition
{
    Left,
    Right
}
