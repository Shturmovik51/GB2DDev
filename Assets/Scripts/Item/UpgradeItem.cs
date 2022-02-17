using UnityEngine;
using UnityEngine.UI;

public class UpgradeItem
{
    public int ItemID { get; }
    public UpgradeType UpgradeType { get; }
    public int ValueUpgrade { get; }
    public Sprite ImageSprite { get; }
    public bool IsActive { get; private set; }
    public bool IsEquipped { get; private set; }
    public Toggle Toggle { get; private set; }

    public UpgradeItem(int itemID, UpgradeType upgradeType, int valueUpgrade, Sprite imageSprite)
    {
        ItemID = itemID;
        UpgradeType = upgradeType;
        ValueUpgrade = valueUpgrade;
        ImageSprite = imageSprite;
    }    

    public void SetToggle(Toggle toggle)
    {
        Toggle = toggle;
    }

    public void ChangeItemActiveStatus(bool status)
    {
        IsActive = status;
    }
    public void ChangeItemEquippedStatus(bool status)
    {
        IsEquipped = status;
    }
}
