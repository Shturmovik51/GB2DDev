using System;
using Features.AbilitiesFeature;
using JetBrains.Annotations;
using Tools;

public class AbilitiesController : BaseController
{
    private readonly IAbilityInventoryModel _inventoryModel;
    private readonly IRepository<int, IAbility> _abilityRepository;
    private readonly IAbilityCollectionView _abilityCollectionView;
    private readonly IAbilityActivator _abilityActivator;

    public AbilitiesController(
        [NotNull] IAbilityActivator abilityActivator,
        [NotNull] IAbilityInventoryModel inventoryModel,
        [NotNull] IRepository<int, IAbility> abilityRepository,
        [NotNull] IAbilityCollectionView abilityCollectionView)
    {
        _abilityActivator = abilityActivator ?? throw new ArgumentNullException(nameof(abilityActivator));
        _inventoryModel = inventoryModel ?? throw new ArgumentNullException(nameof(inventoryModel));
        _abilityRepository = abilityRepository ?? throw new ArgumentNullException(nameof(abilityRepository));
        _abilityCollectionView = abilityCollectionView ?? throw new ArgumentNullException(nameof(abilityCollectionView));
        _abilityCollectionView.UseRequested += OnAbilityUseRequested;
        _abilityCollectionView.Display(_inventoryModel.GetAbilities());
    }

    private void OnAbilityUseRequested(object sender, AbilityItem e)
    {
        if (_abilityRepository.Content.TryGetValue(e.ItemID, out var ability))
            ability.Apply(_abilityActivator, (AbilitiesView) sender);
    }
}