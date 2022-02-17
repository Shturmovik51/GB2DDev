using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeItem", menuName = "UpgradeItem")]
public class UpgradeItemConfig : ScriptableObject
{
    [SerializeField] private ItemConfig _itemConfig;
    [SerializeField] private UpgradeType _upgradeType;
    [SerializeField] private int _valueUpgrade;
    [SerializeField] private Sprite _imageSprite;

    public int Id => _itemConfig.Id;
    public UpgradeType UpgradeType => _upgradeType;
    public int ValueUpgrade => _valueUpgrade;
    public Sprite ImageSprite => _imageSprite;
}

public enum UpgradeType
{
    None = 0,
    Speed = 1,
    Acceleration = 2,
}
