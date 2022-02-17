using System.Collections.Generic;
using Tools;

public class ItemsRepository : BaseController, IRepository<int, IItem>
{
    private readonly List<IItem> _itemsConfig = new List<IItem>();
    private Dictionary<int, IItem> _itemsMapById = new Dictionary<int, IItem>();

    public IReadOnlyDictionary<int, IItem> ItemsMapBuID => _itemsMapById;

    public ItemsRepository(IReadOnlyList<UpgradeItemConfig> upgradeItemConfigs, IReadOnlyList<AbilityItemConfig> abilityItemConfigs)
    {
        foreach (var itemConfig in upgradeItemConfigs)
        {
            var itemProperty = CreateUpgradeItem(itemConfig);
            var newItem = new Item<UpgradeItem>(itemProperty, itemConfig.Id);

            _itemsConfig.Add(newItem);
        }

        foreach (var itemConfig in abilityItemConfigs)
        {
            var itemProperty = CreateAbilityItem(itemConfig);
            var newItem = new Item<AbilityItem>(itemProperty, itemConfig.Item.Id);
            _itemsConfig.Add(newItem);
        }

        PopulateItems(_itemsConfig);
    }

    protected override void OnDispose()
    {
        _itemsMapById.Clear();
    }

    private void PopulateItems(List<IItem> itemsCollection)
    {
        foreach (var item in itemsCollection)
        {
            if (_itemsMapById.ContainsKey(item.ItemID))
                continue;

            _itemsMapById.Add(item.ItemID, item);
        }
    }

    private UpgradeItem CreateUpgradeItem(UpgradeItemConfig config)
    {
        return new UpgradeItem(config.Id, config.UpgradeType, config.ValueUpgrade, config.ImageSprite);        
    }
    private AbilityItem CreateAbilityItem(AbilityItemConfig config)
    {
        return new AbilityItem(config.Item.Id, config.View, config.Type, config.Sprite, config.Value, config.Duration);
    }   
}


