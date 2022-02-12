using System.Collections.Generic;
using UI;
using UnityEngine;

public interface IInventoryView:IView
{
    void Display(IReadOnlyList<UpgradeItem> items);
    void Init(Transform cellPlace);
}
