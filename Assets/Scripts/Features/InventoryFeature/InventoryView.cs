using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryView : IInventoryView
{
    private Transform _cellPlace;
    private GameObject _upgradeItemPref;
    private Action<UpgradeItem> _refreshInventory;
    public InventoryView(Action<UpgradeItem> refreshInventory)
    {
        var handle = ResourceLoader.LoadPrefab(ResourceReferences.UpgradeItemView);
        _upgradeItemPref = (GameObject)handle.Result;
        _refreshInventory = refreshInventory;
    }

    public void Display(IReadOnlyList<IItem> items)
    {
        foreach (var item in items)
        {            
            var itemProperty = item.GetItemProperty<UpgradeItem>();

            if(itemProperty != null)
            {
                var cell = UnityEngine.Object.Instantiate(_upgradeItemPref, _cellPlace);
                var view = cell.GetComponent<UpgradeItemView>();
                view.Init(itemProperty, _refreshInventory);
            }
        }            
    }

    public void Show()
    {
        
    }

    public void Hide()
    {
        
    }

    public void Init(Transform cellPlace)
    {
        _cellPlace = cellPlace;
    }
}
