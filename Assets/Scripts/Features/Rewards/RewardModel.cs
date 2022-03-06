using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine;

public class RewardModel
{
    public int TimeCooldown { get; }
    public int TimeDeadline { get; }
    public List<Reward> Rewards { get; }
    public List<SlotRewardView> RewardSlots { get; private set; }
    public bool IsRewardReceived { get; private set; }
    public Transform ParentTransform { get; }

    public RewardModel(AssetReference assetReference, Transform slotParent)
    {
        var rewardsConfig = ResourceLoader.LoadDataSource<RewardItemConfig>(assetReference);

        Rewards = rewardsConfig.Rewards;
        TimeCooldown = rewardsConfig.TimeCooldown;
        TimeDeadline = rewardsConfig.TimeDeadline;
        ParentTransform = slotParent;
    }

    public void SetRewardReceived (bool value)
    {
        IsRewardReceived = value;
    }

    public void InitSlots(SlotRewardView slotPrefap)
    {
        RewardSlots = new List<SlotRewardView>();

        for (int i = 0; i < Rewards.Count; i++)
        {
            var reward = Rewards[i];
            var slotInstance = GameObject.Instantiate(slotPrefap, ParentTransform, false);
            slotInstance.SetData(reward, PrefsKeys.DayCountTimerKey, i + 1, false);
            RewardSlots.Add(slotInstance);
        }        
    }
}
