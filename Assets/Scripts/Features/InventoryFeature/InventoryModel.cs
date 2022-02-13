using System.Collections.Generic;

public class InventoryModel : IInventoryModel, IAbilityInventoryModel
{
    private readonly List<UpgradeItem> _items = new List<UpgradeItem>();
    private readonly List<AbilityItem> _abilities = new List<AbilityItem>();

    public IReadOnlyList<UpgradeItem> GetEquippedItems()
    {
        return _items;
    }
    public IReadOnlyList<AbilityItem> GetAbilities()
    {
        return _abilities;
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

    public void EquipAbility(AbilityItem ability)
    {
        if (_abilities.Contains(ability))
            return;

        _abilities.Add(ability);
    }

    public void UnEquipAbility(AbilityItem ability)
    {
        _abilities.Remove(ability);
    }
}

public interface IAbilityInventoryModel
{
     IReadOnlyList<AbilityItem> GetAbilities();
    void EquipAbility(AbilityItem ability);
    void UnEquipAbility(AbilityItem ability);
}
