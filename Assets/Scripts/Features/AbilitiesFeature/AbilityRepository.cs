using Features.AbilitiesFeature;
using System;
using System.Collections.Generic;
using Tools;
using UnityEngine;

public class AbilityRepository : BaseController, IRepository<int, IAbility>
{
    public IReadOnlyDictionary<int, IAbility> ItemsMapBuID { get => _abilitiesMap; }

    private Dictionary<int, IAbility> _abilitiesMap = new Dictionary<int, IAbility>();

    public AbilityRepository(IReadOnlyList<IItem> items)
    {
        foreach (var item in items)
        {
            var abilityItem = item.GetItemProperty<AbilityItem>();
            if(abilityItem != null)
                _abilitiesMap[item.ItemID] = CreateAbility(abilityItem);
        }
    }

    private IAbility CreateAbility(AbilityItem config)
    {
        switch (config.Type)
        {
            case AbilityType.None:
                return AbilityStub.Default;
            case AbilityType.Gun:
                var gunAbility = new GunAbility(config.View, config.Value);
                AddController(gunAbility);
                return gunAbility;
            case AbilityType.Shield:
                var shieldAbility = new ShieldAbility(config.View, config.Duration);
                AddController(shieldAbility);
                return shieldAbility;
            case AbilityType.Smoke:
                var smokeAbility = new SmokeAbility(config.View, config.Duration);
                AddController(smokeAbility);
                return smokeAbility;
            case AbilityType.Jump:
                var jumpAbility = new JumpAbility(config.Duration, config.Value);
                AddController(jumpAbility);
                return jumpAbility;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}

public class AbilityStub : BaseController, IAbility
{
    public static AbilityStub Default { get; } = new AbilityStub();

    public void Apply(IAbilityActivator activator)
    {
    }
}