using Saves;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardController : BaseController
{
    private readonly RewardView _rewardView;
    private readonly RewardModel _dailyRewardModel;
    private readonly RewardModel _weeklyRewardModel;
    private SaveDataRepository _saveDataRepository;
    private RewardRefresher _rewardRefresher;
    private ProfilePlayer _profile;

    public RewardController(RewardView rewardView, CurrencyWindow currencyWindow, SaveDataRepository saveDataRepository, ProfilePlayer profile)
    {
        _rewardView = rewardView;

        _dailyRewardModel = new RewardModel(ResourcePaths.DailyRewardsPath, rewardView.DailySlotsParent);
        _weeklyRewardModel = new RewardModel(ResourcePaths.WeeklyRewardsPath, rewardView.WeeklySlotsParent);
        _dailyRewardModel.InitSlots(rewardView.SlotPrefab);
        _weeklyRewardModel.InitSlots(rewardView.SlotPrefab);

        var dailyRewardRefreshModel = new RewardRefresherModel(_dailyRewardModel, profile.RewardData.LastDailyRewardTime, 
                                                            _rewardView.DailyRewardTimer, _rewardView.DailyRewardTimerImage, 
                                                            profile.RewardData.CurrentActiveDailySlot, _rewardView.GetRewardButton);
        var weeklyRewardRefreshModel = new RewardRefresherModel(_weeklyRewardModel, profile.RewardData.LastWeeklyRewardTime,
                                                            _rewardView.WeeklyRewardTimer, _rewardView.WeeklyRewardTimerImage, 
                                                            profile.RewardData.CurrentActiveWeeklySlot, _rewardView.GetRewardButton);
        _rewardRefresher = new RewardRefresher();
        AddController(_rewardRefresher);

        _rewardRefresher.AddModel(dailyRewardRefreshModel);
        _rewardRefresher.AddModel(weeklyRewardRefreshModel);

        _saveDataRepository = saveDataRepository;
        _profile = profile;
        currencyWindow.Init(profile.RewardData.Diamond, profile.RewardData.Wood);
        //saveDataRepository.Load();

        _rewardRefresher.RefreshUi();
        SubscribeButtons();

        AddGameObjects(_rewardView.gameObject);
        AddGameObjects(currencyWindow.gameObject);

        _rewardView.StartCoroutine(UpdateCoroutine());
        _rewardView.ShowDailyRewardsButton.onClick.Invoke();
    }

    private IEnumerator UpdateCoroutine()
    {
        while (true)
        {
            Update();
            yield return new WaitForSeconds(1f);
        }
    }

    private void Update()
    {
        _rewardRefresher.RefreshRewardState();
        _rewardRefresher.RefreshUi();
        //_saveDataRepository.Save();
    }
    
    private void SubscribeButtons()
    {
        _rewardView.GetRewardButton.onClick.AddListener(ClaimReward);
        _rewardView.ResetButton.onClick.AddListener(ResetReward);
        _rewardView.CloseButton.onClick.AddListener(CloseRewardScreen);
        _rewardView.ShowDailyRewardsButton.onClick.AddListener(() => 
                    SetRewardWindow(_rewardView.DailySlotsParent, _rewardView.ShowDailyRewardsButton));
        _rewardView.ShowWeeklyRewardsButton.onClick.AddListener(() => 
                    SetRewardWindow(_rewardView.WeeklySlotsParent, _rewardView.ShowWeeklyRewardsButton));
    }

    private void SetRewardWindow(Transform grid, Button button)
    {
        if (button.image.color == Color.green)
            return;

        _rewardView.ResetRewardsShowCondition();
        button.image.color = Color.green;
        grid.gameObject.SetActive(true);
        _rewardRefresher.RefreshUi();
    }

    private void ResetReward()
    {
        _profile.RewardData.LastDailyRewardTime.Value = null;
        _profile.RewardData.CurrentActiveDailySlot.Value = 0;
        _profile.RewardData.LastWeeklyRewardTime.Value = null; 
        _profile.RewardData.CurrentActiveWeeklySlot.Value = 0;
    }

    private void ClaimReward()
    {
        if (_rewardView.DailySlotsParent.gameObject.activeInHierarchy)
        {
            if (_dailyRewardModel.IsRewardReceived)
                return;

            var reward = _dailyRewardModel.Rewards[_profile.RewardData.CurrentActiveDailySlot.Value];
            switch (reward.Type)
            {
                case RewardType.None:
                    break;
                case RewardType.Wood:
                    _profile.RewardData.Wood.Value += reward.Count;
                    break;
                case RewardType.Diamond:
                    _profile.RewardData.Diamond.Value += reward.Count;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _profile.RewardData.LastDailyRewardTime.Value = DateTime.UtcNow;
            _profile.RewardData.CurrentActiveDailySlot.Value = (_profile.RewardData.CurrentActiveDailySlot.Value + 1) % _dailyRewardModel.Rewards.Count;
        }

        if (_rewardView.WeeklySlotsParent.gameObject.activeInHierarchy)
        {
            if (_weeklyRewardModel.IsRewardReceived)
                return;

            var reward = _weeklyRewardModel.Rewards[_profile.RewardData.CurrentActiveWeeklySlot.Value];
            switch (reward.Type)
            {
                case RewardType.None:
                    break;
                case RewardType.Wood:
                    _profile.RewardData.Wood.Value += reward.Count;
                    break;
                case RewardType.Diamond:
                    _profile.RewardData.Diamond.Value += reward.Count;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _profile.RewardData.LastWeeklyRewardTime.Value = DateTime.UtcNow;
            _profile.RewardData.CurrentActiveWeeklySlot.Value = (_profile.RewardData.CurrentActiveWeeklySlot.Value + 1) % _weeklyRewardModel.Rewards.Count;
        }       
        
        _rewardRefresher.RefreshRewardState();
    }
    private void CloseRewardScreen()
    {
        _profile.CurrentState.Value = Profile.GameState.Start;
    }
}
