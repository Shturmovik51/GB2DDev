using System;
using System.Collections.Generic;
using UnityEngine;

namespace Features.AbilitiesFeature
{
    [RequireComponent(typeof(CanvasGroup))]
    public class AbilitiesView : MonoBehaviour, IAbilityCollectionView
    {
        public event EventHandler<AbilityItem> UseRequested;

        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Transform _layout;
        [SerializeField] private AbilityItemView _viewPrefab;

        public void Show()
        {
            _canvasGroup.alpha = 1;
        }

        public void Hide()
        {
            _canvasGroup.alpha = 0;
        }

        public void Display(IReadOnlyList<IItem> items)
        {
            foreach (var item in items)
            {
                var abilityProperty = item.GetItemProperty<AbilityItem>();
                if(abilityProperty != null)
                {
                    var view = Instantiate<AbilityItemView>(_viewPrefab, _layout);
                    view.Init(abilityProperty);
                    view.OnClick += OnRequested;
                }
            }
        }

        private void OnRequested(AbilityItem obj)
        {
            UseRequested?.Invoke(this, obj);
        }
    }
}