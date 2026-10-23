
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Notification Data", menuName = "Game/Notification Data")]
public class NotificationData : ScriptableObject
{
    [field: SerializeField] public List<Notification> Notifications {get; private set;}
}

[System.Serializable]
public struct Notification
{
    [field: SerializeField] public string Title {get; private set; }
    [field: SerializeField] public string Message {get; private set; }
    [field: SerializeField] public int Hour {get; private set; }
    [field: SerializeField] public int Minute {get; private set;}
}