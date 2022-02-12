using System.Collections.Generic;
using UnityEngine;

public class InventoryView : IInventoryView
{
    private Transform _cellPlace;
    private GameObject _upgradeItemPref;

    public InventoryView()
    {
        _upgradeItemPref = (GameObject)Resources.Load("Prefabs/UpgradeItemView");
    }

    public void Display(IReadOnlyList<UpgradeItem> items)
    {
        foreach(var item in items)
        {
            var cell = Object.Instantiate(_upgradeItemPref, _cellPlace);
            var view = cell.GetComponent<UpgradeItemView>();
            view.Init(item);
        }            
    }

    public void Show()
    {
        throw new System.NotImplementedException();
    }

    public void Hide()
    {
        throw new System.NotImplementedException();
    }

    public void Init(Transform cellPlace)
    {
        _cellPlace = cellPlace;
    }
}
