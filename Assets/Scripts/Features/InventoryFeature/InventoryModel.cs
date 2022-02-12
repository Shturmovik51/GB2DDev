using System.Collections.Generic;

public class InventoryModel : IInventoryModel
{
    private readonly List<UpgradeItem> _items = new List<UpgradeItem>();

    public IReadOnlyList<UpgradeItem> GetEquippedItems()
    {
        return _items;
    }

    public void EquipItem(UpgradeItem item)
    {
        if (_items.Contains(item))
            return;

        _items.Add(item);
    }

    public void UnEquipItem(UpgradeItem item)
    {
        _items.Remove(item);
    }
}
