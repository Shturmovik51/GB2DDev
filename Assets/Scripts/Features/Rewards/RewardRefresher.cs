using System;
using System.Collections.Generic;
using UnityEngine;

public class RewardRefresher: BaseController
{
    private List<RewardRefresherModel> modelsForUpdate;

    public RewardRefresher()
    {
        modelsForUpdate = new List<RewardRefresherModel>();
    }

    public void AddModel(RewardRefresherModel model)
    {
        modelsForUpdate.Add(model);
    }

    public void RefreshRewardState()
    {
        foreach (var model in modelsForUpdate)
        {
            model.RewardModel.SetRewardReceived(false);

            if (model.LastRewardTime.Value.HasValue)
            {
                var timeSpan = DateTime.UtcNow - model.LastRewardTime.Value;
                if (timeSpan.Value.TotalSeconds > model.RewardModel.TimeDeadline)
                {
                    model.SetNullLastRewardTime();
                    model.ActiveSlot.Value = 0;
                }
                else if (timeSpan.Value.TotalSeconds < model.RewardModel.TimeCooldown)
                {
                    model.RewardModel.SetRewardReceived(true);
                }
            }
        }
    }

    public void RefreshUi()
    {
        foreach (var model in modelsForUpdate)
        {

            if (model.RewardModel.ParentTransform.gameObject.activeInHierarchy)
            {
                model.GetRewardButton.interactable = !model.RewardModel.IsRewardReceived;
            }

            for (var i = 0; i < model.RewardModel.Rewards.Count; i++)
            {
                model.RewardModel.RewardSlots[i].SetData(i <= model.ActiveSlot.Value);
            }

            DateTime nextBonusTime = !model.LastRewardTime.Value.HasValue ? DateTime.MinValue : 
                                        model.LastRewardTime.Value.Value.AddSeconds(model.RewardModel.TimeCooldown);

            var dayDelta = nextBonusTime - DateTime.UtcNow;
            if (dayDelta.TotalSeconds < 0)
                dayDelta = new TimeSpan(0);

            model.RewardTimer.text = dayDelta.ToString();

            model.RewardTimerImage.fillAmount = (model.RewardModel.TimeCooldown - (float)dayDelta.TotalSeconds) / model.RewardModel.TimeCooldown;
        }
    }
}
