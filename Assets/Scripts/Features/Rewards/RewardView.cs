using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class RewardView : MonoBehaviour, IView
{
    #region Fields   
    [SerializeField] public TMP_Text DailyRewardTimer;
    [SerializeField] public TMP_Text WeeklyRewardTimer;
    [SerializeField] public Transform DailySlotsParent;
    [SerializeField] public Transform WeeklySlotsParent;
    [SerializeField] public SlotRewardView SlotPrefab;
    [SerializeField] public Button ResetButton;
    [SerializeField] public Button GetRewardButton;
    [SerializeField] public Button ShowDailyRewardsButton;
    [SerializeField] public Button ShowWeeklyRewardsButton;
    [SerializeField] public Button CloseButton;
    [SerializeField] public Image DailyRewardTimerImage;
    [SerializeField] public Image WeeklyRewardTimerImage;
    #endregion

    private Image _defaultButtonImage;

    public void ResetRewardsShowCondition()
    {
        if(_defaultButtonImage == null)
        {
            _defaultButtonImage = ResetButton.image;
        }

        DailySlotsParent.gameObject.SetActive(false);
        WeeklySlotsParent.gameObject.SetActive(false);
        ShowDailyRewardsButton.image.color = _defaultButtonImage.color;
        ShowWeeklyRewardsButton.image.color = _defaultButtonImage.color;
    }


    private void OnDestroy()
    {
        GetRewardButton.onClick.RemoveAllListeners();
        ResetButton.onClick.RemoveAllListeners();
        ShowDailyRewardsButton.onClick.RemoveAllListeners();
        ShowWeeklyRewardsButton.onClick.RemoveAllListeners();
        CloseButton.onClick.RemoveAllListeners();
    }

    public void Hide()
    {
        
    }

    public void Show()
    {
        
    }
}
