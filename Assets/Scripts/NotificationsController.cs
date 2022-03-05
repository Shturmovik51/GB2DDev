using System;
using Unity.Notifications.Android;
using UnityEngine;

public class NotificationsController
{
    private const string AndroidNotificationId = "android_notification_id";
    private const string IOSNotificationId = "ios_notification_id";

    public void CreateNotification(string title)
    {
        var androidSettingsChannel = new AndroidNotificationChannel
        {
            Id = AndroidNotificationId,
            Name = "Notifier",
            Description = "Description Notifier",
            Importance = Importance.High,
            CanBypassDnd = true,
            EnableLights = true,
            CanShowBadge = true,
            EnableVibration = true,
            LockScreenVisibility = LockScreenVisibility.Public
        };

        AndroidNotificationCenter.RegisterNotificationChannel(androidSettingsChannel);

        var androidNotification = new AndroidNotification
        {
            Title = title,
            Color = Color.black,
            FireTime = DateTime.UtcNow
        };

        var sendId = AndroidNotificationCenter.SendNotification(androidNotification, AndroidNotificationId);
    }
}
