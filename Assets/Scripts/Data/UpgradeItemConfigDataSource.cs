using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeItemConfigDataSource", menuName = "UpgradeItemConfigDataSource")]
public class UpgradeItemConfigDataSource : ScriptableObject
{
    [SerializeField]
    private UpgradeItemConfig[] _upgradesConfigs;

    public UpgradeItemConfig[] UpgradesConfigs => _upgradesConfigs;
}

public class BaseDataSource<T> : ScriptableObject where T:ScriptableObject
{
    [SerializeField] private T[] _content;

    public T[] Content => _content;

}