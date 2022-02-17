using System.Collections.Generic;

public class InventoryModel : IInventoryModel//, IAbilityInventoryModel
{
    private readonly List<IItem> _quippedItems = new List<IItem>();

    public IReadOnlyList<IItem> GetEquippedItems()
    {
        return _quippedItems;
    }

    public void EquipItem(IItem item)
    {
        if (_quippedItems.Contains(item))
            return;

        _quippedItems.Add(item);
    }
    public void UnEquipItem(IItem item)
    {
        _quippedItems.Remove(item);
    }
}

//public interface IAbilityInventoryModel
//{
//     IReadOnlyList<AbilityItem> GetAbilities();
//    void EquipAbility(AbilityItem ability);
//    void UnEquipAbility(AbilityItem ability);
//}
