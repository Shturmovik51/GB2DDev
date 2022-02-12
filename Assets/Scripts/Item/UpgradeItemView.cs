using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UpgradeItemView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Button _button;
    [SerializeField] private Image _buttonImage;
    [SerializeField] private GameObject _descriptionObj;
    [SerializeField] private TextMeshProUGUI _upgradeTupeText;
    [SerializeField] private TextMeshProUGUI _upgradeValueText;
    [SerializeField] private Toggle _toggle;

    public Action<UpgradeItem> OnClick;

    private UpgradeItem _item;

    public void Init(UpgradeItem item, Action<UpgradeItem> refresh)
    {
        _item = item;        
        _buttonImage.sprite = item.ImageSprite;
        _upgradeTupeText.text = item.UpgradeType.ToString();
        _upgradeValueText.text = item.ValueUpgrade.ToString();
        _item.SetToggle(_toggle);
        OnClick += refresh;
    }

    private void Awake()
    {
        _button.onClick.AddListener(Click);
    }

    private void Click()
    {
        OnClick?.Invoke(_item);
        _toggle.isOn = !_toggle.isOn;
        _item.ChangeItemActiveStatus(_toggle.isOn);
    }

    private void OnDestroy()
    {
        OnClick = null;
        _button.onClick.RemoveAllListeners();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _descriptionObj.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _descriptionObj.SetActive(false);
    }
}
