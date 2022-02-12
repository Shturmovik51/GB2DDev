using System.Collections.Generic;

public interface IInventoryModel
{
    IReadOnlyList<UpgradeItem> GetEquippedItems();
    void EquipItem(UpgradeItem item);
    void UnEquipItem(UpgradeItem item);
}
