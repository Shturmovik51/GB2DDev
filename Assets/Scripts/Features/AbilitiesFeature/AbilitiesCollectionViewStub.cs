using System;
using System.Collections.Generic;
using UnityEngine;

namespace Features.AbilitiesFeature
{
    public class AbilitiesCollectionViewStub : IAbilityCollectionView
    {
        public event EventHandler<AbilityItem> UseRequested;
        public void Display(IReadOnlyList<AbilityItem> abilityItems)
        {
            foreach (var item in abilityItems)
            {
                Debug.Log($"Equiped item : {item.ItemID}");
                UseRequested?.Invoke(this, item);
            }
        }

        public void Show()
        {
            //throw new NotImplementedException();
        }

        public void Hide()
        {
            //throw new NotImplementedException();
        }
    }
}