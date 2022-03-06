using Profile;
using Saves;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MainController : BaseController
{
    private MainMenuController _mainMenuController;
    private ShedController _shedController;
    private GameController _gameController;
    private RewardController _rewardController;
    private InventoryController _inventoryController;
    private readonly Transform _placeForUi;
    private readonly ProfilePlayer _profilePlayer;
    private FightController _fightController;
    private IReadOnlyList<UpgradeItemConfig> _upgradeItemConfigs;
    private IReadOnlyList<AbilityItemConfig> _abilityItemConfigs;

    private InventoryModel _inventoryModel;

    public MainController(Transform placeForUi, ProfilePlayer profilePlayer)
    {
        _placeForUi = placeForUi;
        _profilePlayer = profilePlayer;

        _upgradeItemConfigs = ResourceLoader.LoadDataSource<UpgradeItemConfigDataSource>(ResourceReferences.UpgradeSource).UpgradesConfigs;
        _abilityItemConfigs = ResourceLoader.LoadDataSource<AbilityItemConfigDataSource>(ResourceReferences.AbilitiesSource).AbilitiesConfigs;
         
        OnChangeGameState(_profilePlayer.CurrentState.Value);
        _profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);
    }

    protected override void OnDispose()
    {
        AllClear();

        _profilePlayer.CurrentState.UnSubscriptionOnChange(OnChangeGameState);
        base.OnDispose();
    }

    private void OnChangeGameState(GameState state)
    {
        switch (state)
        {
            case GameState.Start:
                Debug.Log(_upgradeItemConfigs);
                _mainMenuController = new MainMenuController(_placeForUi, _profilePlayer);
                _gameController?.Dispose(); 
                _rewardController?.Dispose();
                // _inventoryController?.Dispose();                
                break;

            case GameState.Rewards:
                _rewardController = CreateRewardController();
                _mainMenuController?.Dispose();
                break;

            case GameState.Garage:
                _inventoryModel = new InventoryModel();
                _inventoryController = new InventoryController(_upgradeItemConfigs, _abilityItemConfigs, _inventoryModel);
                _shedController = new ShedController(_upgradeItemConfigs, _profilePlayer, _inventoryModel, _inventoryController, _placeForUi);                
                _shedController.Enter();
                _mainMenuController?.Dispose();
                break;

            case GameState.Game:
                _shedController.Exit();
                _gameController = new GameController(_profilePlayer, _inventoryModel, _placeForUi, _shedController);
                _fightController?.Dispose();
                break;

            case GameState.Fight:
                _fightController = CreateFightController();                
                _gameController?.Dispose();
                break;
            default:
                AllClear();
                break;
        }
    }    

    private RewardController CreateRewardController()
    {
        var rewardViewHandle = ResourceLoader.LoadAndInstantiatePrefab(ResourceReferences.RewardWindow, _placeForUi);
        var saveDataRepository = new SaveDataRepository();
        saveDataRepository.Initialization();
        var currencyViewHandle = ResourceLoader.LoadAndInstantiatePrefab(ResourceReferences.CurrencyWindow, _placeForUi);
        var controller = new RewardController(rewardViewHandle, currencyViewHandle, saveDataRepository, _profilePlayer);
        AddController(controller);
        return controller;
    }

    private FightController CreateFightController()
    {
        var fightViewHandle = ResourceLoader.LoadAndInstantiatePrefab(ResourceReferences.FightWindowView, _placeForUi);        
        var fightController = new FightController(fightViewHandle, _profilePlayer);
        AddController(fightController);
        return fightController;
    }


    private void AllClear()
    {
        _inventoryController?.Dispose();
        _mainMenuController?.Dispose();
        _gameController?.Dispose();
    }    
}