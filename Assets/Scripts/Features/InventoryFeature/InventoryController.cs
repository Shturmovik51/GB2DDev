using System.Collections.Generic;
using Tools;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : BaseController, IInventoryController
{
    private readonly IInventoryModel _inventoryModel;
    private readonly IInventoryView _inventoryView;
    private readonly ItemsRepository _itemsRepository;

    public ItemsRepository ItemsRepository => _itemsRepository;

    public InventoryController( IReadOnlyList<UpgradeItemConfig> upgradeItemConfigs, 
                                IReadOnlyList<AbilityItemConfig> abilityItemConfigs, 
                                InventoryModel inventoryModel)
    {
        _inventoryModel = inventoryModel;
        _inventoryView = new InventoryView(RefreshInventory);
        _itemsRepository = new ItemsRepository(upgradeItemConfigs, abilityItemConfigs);
    }

    public void InitInventoryView(Transform cellPlace)
    {
        _inventoryView.Init(cellPlace);
    }

    public void RefreshInventory(UpgradeItem item)
    {
        foreach (var currentItem in _itemsRepository.ItemsMapBuID)
        {
            var itemProperty = currentItem.Value.GetItemProperty<UpgradeItem>();

            if(itemProperty != null)            
            {               
                if (itemProperty == item)
                    continue;

                if (itemProperty.UpgradeType == item.UpgradeType && itemProperty.IsActive)
                {
                    itemProperty.ChangeItemActiveStatus(false);
                    itemProperty.Toggle.isOn = false;
                }
            }
        }
    }

    public void ShowInventory()
    {
        foreach (var item in _itemsRepository.ItemsMapBuID.Values)
        {

            _inventoryModel.EquipItem(item);        
        }

        var equippedItems = _inventoryModel.GetEquippedItems();
        _inventoryView.Display(equippedItems);
    }
}
