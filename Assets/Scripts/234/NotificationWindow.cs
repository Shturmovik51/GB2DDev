using System;
using Unity.Notifications.Android;
using Unity.Notifications.iOS;
using UnityEngine;
using UnityEngine.UI;

public class NotificationWindow : MonoBehaviour
{
    private const string AndroidNotificationId = "android_notification_id";
    private const string IOSNotificationId = "ios_notification_id";

    [SerializeField] private Button _showNotificationButton;

    private void Start()
    {
        _showNotificationButton.onClick.AddListener(CreateNotifications);
    }

    private void OnDestroy()
    {
        _showNotificationButton.onClick.RemoveAllListeners();
    }

    private void CreateNotifications()
    {
        if (Application.platform == RuntimePlatform.Android)
            CreateNotificationAndroid();
        else if (Application.platform == RuntimePlatform.OSXPlayer)
        {
            CreateNotificationIOS();
        }
    }

    private void CreateNotificationAndroid()
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
            Title = "Long time no see",
            Color = Color.black,
            RepeatInterval = TimeSpan.FromDays(1)
        };

        var sendId = AndroidNotificationCenter.SendNotification(androidNotification, AndroidNotificationId);
    }

    private void CreateNotificationIOS()
    {
        var iosNotification = new iOSNotification
        {
            Identifier = IOSNotificationId,
            Title = "IOS Notifier",
            Subtitle = "Subtitle IOS Notifier",
            Body = "Description IOS Notifier",
            Data = "22/09/2021",
            ForegroundPresentationOption = PresentationOption.Alert | PresentationOption.Badge | PresentationOption.Sound,
        };
                
        iOSNotificationCenter.ScheduleNotification(iosNotification);
    }
}
