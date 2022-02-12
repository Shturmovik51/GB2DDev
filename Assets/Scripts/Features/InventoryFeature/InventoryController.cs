using System.Collections.Generic;
using Tools;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : BaseController, IInventoryController
{
    private readonly IInventoryModel _inventoryModel;
    private readonly IInventoryView _inventoryView;
    private readonly IRepository<int, UpgradeItem> _itemsRepository;

    public InventoryController(IReadOnlyList<UpgradeItemConfig> itemConfigs, InventoryModel inventoryModel)
    {
        _inventoryModel = inventoryModel;
        _inventoryView = new InventoryView(RefreshInventory);
        _itemsRepository = new ItemsRepository(itemConfigs);

    }

    public void InitInventoryView(Transform cellPlace)
    {
        _inventoryView.Init(cellPlace);
    }

    public void RefreshInventory(UpgradeItem item)
    {  
        foreach (var currentItem in _itemsRepository.Content.Values)
        {
            if (currentItem == item)
                continue;

            if (currentItem.UpgradeType == item.UpgradeType && currentItem.IsActive)
            {
                currentItem.ChangeItemActiveStatus(false);
                //currentItem.ChangeItemEquippedStatus(false);
                currentItem.Toggle.isOn = false;
            }
        }
    }


    public void ShowInventory()
    {
        foreach (var item in _itemsRepository.Content.Values)
            _inventoryModel.EquipItem(item);        

        var equippedItems = _inventoryModel.GetEquippedItems();
        _inventoryView.Display(equippedItems);
    }

    public IRepository<int, UpgradeItem> GetItemRepository()
    {
        return _itemsRepository;
    }
}
