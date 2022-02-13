using System;
using UnityEngine;
using UnityEngine.UI;

namespace Features.AbilitiesFeature
{
    public class AbilityItemView:MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _image;

        public event Action<AbilityItem> OnClick;

        private AbilityItem _item;

        public void Init(AbilityItem item)
        {
            _item = item;
            _image.sprite = item.ImageSprite;
        }

        private void Awake()
        {
            _button.onClick.AddListener(Click);
        }

        private void Click()
        {
            OnClick?.Invoke(_item);
        }

        private void OnDestroy()
        {
            OnClick = null;
            _button.onClick.RemoveAllListeners();
        }
    }
}