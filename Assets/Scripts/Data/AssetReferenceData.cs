using UnityEngine.AddressableAssets;
using UnityEngine;

[CreateAssetMenu (menuName = "AssetReferenceData", fileName = nameof(AssetReferenceData))]
public class AssetReferenceData : ScriptableObject
{    
    [field: Header("Abilities")]
    [field: SerializeField] public AssetReferenceGameObject AbilitiesView { get; private set; }
    [field: SerializeField] public AssetReferenceGameObject AbilityItemView { get; private set; }
    [field: SerializeField] public AssetReferenceGameObject CannonBomb { get; private set; }
    [field: SerializeField] public AssetReferenceGameObject Shield { get; private set; }
    [field: SerializeField] public AssetReferenceGameObject Smoke { get; private set; }

    [field: Header("Fight")]
    [field: SerializeField] public AssetReferenceGameObject FightButton { get; private set; }
    [field: SerializeField] public AssetReferenceGameObject FightWindowView { get; private set; }

    [field: Header("Rewards")]
    [field: SerializeField] public AssetReferenceGameObject ContainerCountCurrency { get; private set; }
    [field: SerializeField] public AssetReferenceGameObject ContainerSlotReward { get; private set; }
    [field: SerializeField] public AssetReferenceGameObject CurrencyWindow { get; private set; }
    [field: SerializeField] public AssetReferenceGameObject RewardWindow { get; private set; }

    [field: Header("Game")]
    [field: SerializeField] public AssetReferenceGameObject BackGround { get; private set; }
    [field: SerializeField] public AssetReferenceGameObject Car { get; private set; }
    [field: SerializeField] public AssetReferenceGameObject EndlessMove { get; private set; }
    [field: SerializeField] public AssetReferenceGameObject FloatStick { get; private set; }    
    [field: SerializeField] public AssetReferenceGameObject PauseButton { get; private set; }
    [field: SerializeField] public AssetReferenceGameObject StickControl { get; private set; }

    [field: Header("Shed")]
    [field: SerializeField] public AssetReferenceGameObject GarageMenu { get; private set; }
    [field: SerializeField] public AssetReferenceGameObject UpgradeItemView { get; private set; }

    [field: Header("Main")]
    [field: SerializeField] public AssetReferenceGameObject MainMenu { get; private set; }

    [field: Header("Configs")]
    [field: SerializeField] public AssetReference DailyRewards { get; private set; }
    [field: SerializeField] public AssetReference WeeklyRewards { get; private set; }
    [field: SerializeField] public AssetReference UpgradeSource { get; private set; }
    [field: SerializeField] public AssetReference AbilitiesSource { get; private set; }
}
