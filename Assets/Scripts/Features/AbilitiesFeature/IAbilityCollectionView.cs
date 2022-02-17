using System;
using System.Collections.Generic;
using UI;

public interface IAbilityCollectionView: IView
{
    event EventHandler<AbilityItem> UseRequested;
    void Display(IReadOnlyList<IItem> abilityItems);
}