using System.Collections;
using Code.Utils;
using Unity.Notifications.Android;
using UnityEngine;

public class NotificationManager : Singleton<NotificationManager>
{
    [SerializeField] private NotificationData _notificationData;
    
    private IEnumerator Start()
    {
        RegisterNotificationChannel();

        var permissionRequest = new PermissionRequest();

        while (permissionRequest.Status == PermissionStatus.RequestPending)
        {
            yield return null;
        }

        if (permissionRequest.Status != PermissionStatus.Allowed)
        {
            Debug.LogError("Notification permission was not granted.");
            yield break;
        }

        ScheduleDailyReminder();
    }

    private void ScheduleDailyReminder()
    {
        throw new System.NotImplementedException();
    }

    private void RegisterNotificationChannel()
    {
        var notificationChannel = new AndroidNotificationChannel
        {
            Id = "NotificationChannel",
            Name = "Notification Channel",
            Description = "Notification Channel",
            Importance = Importance.Default,
        };
        
        AndroidNotificationCenter.RegisterNotificationChannel(notificationChannel);
    }
}
