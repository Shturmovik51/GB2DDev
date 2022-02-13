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

        private List<AbilityItemView> _currentViews = new List<AbilityItemView>();

        public void Show()
        {
            _canvasGroup.alpha = 1;
        }

        public void Hide()
        {
            _canvasGroup.alpha = 0;
        }


        public void Display(IReadOnlyList<AbilityItem> abilityItems)
        {
            foreach (var ability in abilityItems)
            {
                var view = Instantiate<AbilityItemView>(_viewPrefab, _layout);
                view.Init(ability);
                view.OnClick += OnRequested;
            }
        }

        private void OnRequested(AbilityItem obj)
        {
            UseRequested?.Invoke(this, obj);
        }
    }
}