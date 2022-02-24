using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Tools;
using UI;
using UnityEngine;

public class CurrencyWindow : MonoBehaviour, IView
{  
    [SerializeField] private TextMeshProUGUI _diamondText;
    [SerializeField] private TextMeshProUGUI _woodText;

    private IReadOnlySubscriptionProperty<int> _diamondCount;
    private IReadOnlySubscriptionProperty<int> _woodCount;

    public void Init(IReadOnlySubscriptionProperty<int> diamondCount, IReadOnlySubscriptionProperty<int> woodCount)
    {
        diamondCount.SubscribeOnChange(OnDiamondChange);
        woodCount.SubscribeOnChange(OnWoodChange);
        _diamondCount = diamondCount;
        _woodCount = woodCount;
        OnWoodChange(_woodCount.Value);
        OnDiamondChange(_diamondCount.Value);
    }

    private void OnWoodChange(int val)
    {
        _woodText.text = val.ToString();
    }

    private void OnDiamondChange(int val)
    {
        _diamondText.text = val.ToString();
    }

    private void OnDestroy()
    {
        _diamondCount.UnSubscriptionOnChange(OnDiamondChange);
        _woodCount.UnSubscriptionOnChange(OnWoodChange);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);

    }
}
