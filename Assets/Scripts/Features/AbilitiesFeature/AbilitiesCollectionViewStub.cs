using System;
using System.Collections.Generic;
using UnityEngine;

namespace Features.AbilitiesFeature
{
    public class AbilitiesCollectionViewStub : IAbilityCollectionView
    {
        public event EventHandler<AbilityItem> UseRequested;

        public void Display(IReadOnlyList<IItem> items)
        {
            foreach (var item in items)
            {
                var abilityProperty = item.GetItemProperty<AbilityItem>();
                if (abilityProperty != null)
                {
                    Debug.Log($"Equiped item : {item.ItemID}");
                    UseRequested?.Invoke(this, abilityProperty);
                }
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