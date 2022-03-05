using System;
using Features.AbilitiesFeature;
using JetBrains.Annotations;
using Tools;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AbilitiesController : BaseController
{
    private readonly IInventoryModel _inventoryModel;
    private readonly IRepository<int, IAbility> _abilityRepository;
    private readonly IAbilityCollectionView _abilityCollectionView;
    private readonly IAbilityActivator _abilityActivator;

    public AbilitiesController(
        [NotNull] IAbilityActivator abilityActivator,
        [NotNull] IInventoryModel inventoryModel,
        [NotNull] IRepository<int, IAbility> abilityRepository,
        [NotNull] AsyncOperationHandle<GameObject> abilityCollectionViewHandle)
    {
        _abilityActivator = abilityActivator ?? throw new ArgumentNullException(nameof(abilityActivator));
        _inventoryModel = inventoryModel ?? throw new ArgumentNullException(nameof(inventoryModel));
        _abilityRepository = abilityRepository ?? throw new ArgumentNullException(nameof(abilityRepository));
        _abilityCollectionView = abilityCollectionViewHandle.Result.GetComponent<IAbilityCollectionView>() ?? throw new ArgumentNullException(nameof(abilityCollectionViewHandle));
        _abilityCollectionView.UseRequested += OnAbilityUseRequested;
        _abilityCollectionView.Display(_inventoryModel.GetEquippedItems());

        AddAsyncHandle(abilityCollectionViewHandle);
    }

    private void OnAbilityUseRequested(object sender, AbilityItem e)
    {
        if (_abilityRepository.ItemsMapBuID.TryGetValue(e.ItemID, out var ability))
            ability.Apply(_abilityActivator);
    }
}