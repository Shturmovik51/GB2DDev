using System.Collections.Generic;
using Tools;

public class ItemsRepository : BaseController, IRepository<int, UpgradeItem>
{
    public IReadOnlyDictionary<int, UpgradeItem> Content => _itemsMapById;

    private Dictionary<int, UpgradeItem> _itemsMapById = new Dictionary<int, UpgradeItem>();

    public ItemsRepository(IReadOnlyList<UpgradeItemConfig> upgradeItemConfigs)
    {
        PopulateItems(upgradeItemConfigs);
    }

    protected override void OnDispose()
    {
        _itemsMapById.Clear();
    }

    private void PopulateItems(IReadOnlyList<UpgradeItemConfig> upgradeItemConfigs)
    {
        foreach (var config in upgradeItemConfigs)
        {
            if (_itemsMapById.ContainsKey(config.Id))
                continue;

            _itemsMapById.Add(config.Id, CreateItem(config));
        }
    }

    private UpgradeItem CreateItem(UpgradeItemConfig itemConfig)
    {
        return new UpgradeItem(itemConfig.Id, itemConfig.UpgradeType, itemConfig.ValueUpgrade, itemConfig.ImageSprite);        
    }

    public Dictionary<int, UpgradeItem> GetItemsMapBbyId()
    {
        return _itemsMapById;
    }
}


