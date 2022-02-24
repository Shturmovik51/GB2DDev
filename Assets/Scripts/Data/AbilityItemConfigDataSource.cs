using UnityEngine;

[CreateAssetMenu(fileName = "AbilityItemConfigDataSource", menuName = nameof(AbilityItemConfigDataSource))]
public class AbilityItemConfigDataSource : ScriptableObject
{
    [SerializeField]
    private AbilityItemConfig[] _abilitiesConfigs;

    public AbilityItemConfig[] AbilitiesConfigs => _abilitiesConfigs;
}
