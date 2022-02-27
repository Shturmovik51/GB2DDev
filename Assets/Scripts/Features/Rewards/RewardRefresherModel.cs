using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Tools;
using UnityEngine;
using UnityEngine.UI;

public class RewardRefresherModel
{
    public RewardModel RewardModel { get; }
    public Button GetRewardButton { get; }
    public TMP_Text RewardTimer { get; }
    public Image RewardTimerImage { get; }
    public SubscriptionProperty<int> ActiveSlot { get; }
    public SubscriptionProperty<DateTime?> LastRewardTime { get; private set; }
    public RewardRefresherModel(RewardModel model, SubscriptionProperty<DateTime?> lastRewardTime, TMP_Text rewardTimer, 
                                    Image rewardTimerImage, SubscriptionProperty<int> activeSlot, Button getRewardButton)
    {
        RewardModel = model;
        LastRewardTime = lastRewardTime;
        RewardTimer = rewardTimer;
        RewardTimerImage = rewardTimerImage;
        ActiveSlot = activeSlot;
        GetRewardButton = getRewardButton;
    }

    public void SetNullLastRewardTime()
    {
        LastRewardTime.Value = null;
    }
}
