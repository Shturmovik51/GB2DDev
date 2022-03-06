
using UnityEngine.AddressableAssets;
using UnityEngine;

public static class ResourceReferences
{
    public static AssetReferenceGameObject AbilitiesView { get; }
    public static AssetReferenceGameObject AbilityItemView { get; }
    public static AssetReferenceGameObject CannonBomb { get; }
    public static AssetReferenceGameObject Shield { get; private set; }
    public static AssetReferenceGameObject Smoke { get; private set; }           
    public static AssetReferenceGameObject FightButton { get; private set; }
    public static AssetReferenceGameObject FightWindowView { get; private set; }       
    public static AssetReferenceGameObject ContainerCountCurrency { get; private set; }
    public static AssetReferenceGameObject ContainerSlotReward { get; private set; }
    public static AssetReferenceGameObject CurrencyWindow { get; private set; }
    public static AssetReferenceGameObject RewardWindow { get; private set; }      
    public static AssetReferenceGameObject BackGround { get; private set; }
    public static AssetReferenceGameObject Car { get;  set; }
    public static AssetReferenceGameObject EndlessMove { get; private set; }
    public static AssetReferenceGameObject FloatStick { get; private set; }
    public static AssetReferenceGameObject PauseButton { get; private set; }
    public static AssetReferenceGameObject StickControl { get; private set; }          
    public static AssetReferenceGameObject GarageMenu { get; private set; }
    public static AssetReferenceGameObject UpgradeItemView { get; private set; }
    public static AssetReferenceGameObject MainMenu { get; private set; }
    public static AssetReference DailyRewards { get; private set; }
    public static AssetReference WeeklyRewards { get; private set; }
    public static AssetReference UpgradeSource { get; private set; }
    public static AssetReference AbilitiesSource { get; private set; }

    static ResourceReferences()
    {
        var assetReferenceData = Resources.Load<AssetReferenceData>("AssetReferenceData");

        AbilitiesView = assetReferenceData.AbilitiesView;
        AbilityItemView = assetReferenceData.AbilityItemView;
        CannonBomb = assetReferenceData.CannonBomb;
        Shield = assetReferenceData.Shield;
        Smoke = assetReferenceData.Smoke;
        FightButton = assetReferenceData.FightButton;
        FightWindowView = assetReferenceData.FightWindowView;
        ContainerCountCurrency = assetReferenceData.ContainerCountCurrency;
        ContainerSlotReward = assetReferenceData.ContainerSlotReward;
        CurrencyWindow = assetReferenceData.CurrencyWindow;
        RewardWindow = assetReferenceData.RewardWindow;
        BackGround = assetReferenceData.BackGround;
        Car = assetReferenceData.Car;
        EndlessMove = assetReferenceData.EndlessMove;
        FloatStick = assetReferenceData.FloatStick;
        PauseButton = assetReferenceData.PauseButton;
        StickControl = assetReferenceData.StickControl;
        GarageMenu = assetReferenceData.GarageMenu;
        UpgradeItemView = assetReferenceData.UpgradeItemView;
        MainMenu = assetReferenceData.MainMenu;
        DailyRewards = assetReferenceData.DailyRewards;
        WeeklyRewards = assetReferenceData.WeeklyRewards;
        UpgradeSource = assetReferenceData.UpgradeSource;
        AbilitiesSource = assetReferenceData.AbilitiesSource;
    }
}
