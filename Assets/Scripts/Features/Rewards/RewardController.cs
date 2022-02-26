using Profile;
using Saves;
using System;
//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardController : BaseController
{
    private readonly RewardView _rewardView;
    private List<SlotRewardView> _daylySlots;
    private List<SlotRewardView> _weeklySlots;
    private SaveDataRepository _saveDataRepository;
    private readonly ProfilePlayer _profile;
    private bool _dailyRewardReceived = false;
    private bool _weeklyRewardReceived = false;

    public RewardController(RewardView rewardView, CurrencyWindow currencyWindow, SaveDataRepository saveDataRepository, ProfilePlayer profile)
    {
        _rewardView = rewardView;
        _saveDataRepository = saveDataRepository;
        _profile = profile;
        currencyWindow.Init(profile.RewardData.Diamond, profile.RewardData.Wood);
        //saveDataRepository.Load();

        InitSlots();
        RefreshUi();
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
            yield return new WaitForSeconds(1);
        }
    }

    private void Update()
    {
        RefreshRewardState();
        RefreshUi();
        //_saveDataRepository.Save();
    }

    private void RefreshRewardState()
    {
        _dailyRewardReceived = false;
        _weeklyRewardReceived = false;

        if (_profile.RewardData.LastDailyRewardTime.Value.HasValue)
        {
            Refresh(_profile.RewardData.LastDailyRewardTime.Value, ref _rewardView.DayTimeDeadline, 
                    _profile.RewardData.CurrentActiveDailySlot.Value, ref _rewardView.DayTimeCooldown, ref _dailyRewardReceived);          
        }

        if (_profile.RewardData.LastWeeklyRewardTime.Value.HasValue)
        {
            Refresh(_profile.RewardData.LastWeeklyRewardTime.Value, ref _rewardView.WeekTimeDeadline,
                   _profile.RewardData.CurrentActiveWeeklySlot.Value, ref _rewardView.WeekTimeCooldown, ref _weeklyRewardReceived);
        }

        void Refresh(DateTime? lastRewardTime, ref int timeDeadLine, int activeSlot, ref int timeCD, ref bool isRewardReceived)
        {
            var timeSpan = DateTime.UtcNow - lastRewardTime.Value;
            if (timeSpan.Seconds > timeDeadLine)
            {
                lastRewardTime = null;
                activeSlot = 0;
            }
            else if (timeSpan.Seconds < timeCD)
            {
                isRewardReceived = true;
            }
        }
    }

    private void RefreshUi()
    {
        if (_rewardView.DailySlotsParent.gameObject.activeInHierarchy)
        {
            _rewardView.GetRewardButton.interactable = !_dailyRewardReceived;
        }

        if (_rewardView.WeeklySlotsParent.gameObject.activeInHierarchy)
        {
            _rewardView.GetRewardButton.interactable = !_weeklyRewardReceived;
        }

        for (var i = 0; i < _rewardView.DailyRewards.Count; i++)
        {
            _daylySlots[i].SetData(i <= _profile.RewardData.CurrentActiveDailySlot.Value);
        }

        for (var i = 0; i < _rewardView.WeeklyRewards.Count; i++)
        {
            _weeklySlots[i].SetData(i <= _profile.RewardData.CurrentActiveWeeklySlot.Value);
        }

        DateTime nextDailyBonusTime = !_profile.RewardData.LastDailyRewardTime.Value.HasValue ? DateTime.MinValue
                : _profile.RewardData.LastDailyRewardTime.Value.Value.AddSeconds(_rewardView.DayTimeCooldown);

        var dayDelta = nextDailyBonusTime - DateTime.UtcNow;
        if (dayDelta.TotalSeconds < 0)
            dayDelta = new TimeSpan(0);

        _rewardView.DailyRewardTimer.text = dayDelta.ToString();

        _rewardView.DailyRewardTimerImage.fillAmount = 
                    (_rewardView.DayTimeCooldown - (float)dayDelta.TotalSeconds) / _rewardView.DayTimeCooldown;

        DateTime nextWeeklyBonusTime = !_profile.RewardData.LastWeeklyRewardTime.Value.HasValue ? DateTime.MinValue
                : _profile.RewardData.LastWeeklyRewardTime.Value.Value.AddSeconds(_rewardView.WeekTimeCooldown);

        var weekDelta = nextWeeklyBonusTime - DateTime.UtcNow;
        if (weekDelta.TotalSeconds < 0)
            weekDelta = new TimeSpan(0);

        _rewardView.WeeklyRewardTimer.text = weekDelta.ToString();
        _rewardView.WeeklyRewardTimerImage.fillAmount =
                    (_rewardView.WeekTimeCooldown - (float)weekDelta.TotalSeconds) / _rewardView.WeekTimeCooldown;
    }

    private void InitSlots()
    {
        _daylySlots = new List<SlotRewardView>();
        _weeklySlots = new List<SlotRewardView>();

        for (int i = 0; i < _rewardView.DailyRewards.Count; i++)
        {
            var reward = _rewardView.DailyRewards[i];
            var slotInstance = GameObject.Instantiate(_rewardView.SlotPrefab, _rewardView.DailySlotsParent, false);
            slotInstance.SetData(reward, PrefsKeys.DayCountTimerKey, i + 1, false);
            _daylySlots.Add(slotInstance);
        }
        for (int i = 0; i < _rewardView.WeeklyRewards.Count; i++)
        {
            var reward = _rewardView.WeeklyRewards[i];
            var slotInstance = GameObject.Instantiate(_rewardView.SlotPrefab, _rewardView.WeeklySlotsParent, false);
            slotInstance.SetData(reward, PrefsKeys.WeekCountTimerKey, i + 1, false);
            _weeklySlots.Add(slotInstance);
        }
    }

    private void SubscribeButtons()
    {
        _rewardView.GetRewardButton.onClick.AddListener(ClaimReward);
        _rewardView.ResetButton.onClick.AddListener(ResetReward);
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
        RefreshUi();
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
            if (_dailyRewardReceived)
                return;

            var reward = _rewardView.DailyRewards[_profile.RewardData.CurrentActiveDailySlot.Value];
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
            _profile.RewardData.CurrentActiveDailySlot.Value = (_profile.RewardData.CurrentActiveDailySlot.Value + 1) % _rewardView.DailyRewards.Count;
        }

        if (_rewardView.WeeklySlotsParent.gameObject.activeInHierarchy)
        {
            if (_weeklyRewardReceived)
                return;

            var reward = _rewardView.WeeklyRewards[_profile.RewardData.CurrentActiveWeeklySlot.Value];
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
            _profile.RewardData.CurrentActiveWeeklySlot.Value = (_profile.RewardData.CurrentActiveWeeklySlot.Value + 1) % _rewardView.WeeklyRewards.Count;
        }       
        
        RefreshRewardState();
    }
}
