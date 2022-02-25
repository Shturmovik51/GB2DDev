using Profile;
using Saves;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MainController : BaseController
{
    private MainMenuController _mainMenuController;
    private ShedController _shedController;
    private GameController _gameController;
    private InventoryController _inventoryController;
    private readonly Transform _placeForUi;
    private readonly ProfilePlayer _profilePlayer;
    private FightController _fightController;
    private ResourcePath _rewardView = new ResourcePath { PathResource = "Prefabs/Rewards/RewardWindow" };
    private ResourcePath _fightView = new ResourcePath { PathResource = "Prefabs/FightWindowView" };
    private ResourcePath _currencyView = new ResourcePath { PathResource = "Prefabs/Rewards/CurrencyWindow" };
    private readonly IReadOnlyList<UpgradeItemConfig> _upgradeItemConfigs;
    private readonly IReadOnlyList<AbilityItemConfig> _abilityItemConfigs;

    private InventoryModel _inventoryModel;

    public MainController(Transform placeForUi, ProfilePlayer profilePlayer)
    {
        _placeForUi = placeForUi;
        _profilePlayer = profilePlayer;        
        var upgradeSource = (UpgradeItemConfigDataSource)Resources.Load("Data/UpgradeSource");
        var abilitiesSource = (AbilityItemConfigDataSource)Resources.Load("Data/AbilitiesSource");
        _upgradeItemConfigs = upgradeSource.UpgradesConfigs;
        _abilityItemConfigs = abilitiesSource.AbilitiesConfigs;

        OnChangeGameState(_profilePlayer.CurrentState.Value);
        profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);
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
                _mainMenuController = new MainMenuController(_placeForUi, _profilePlayer);
                _gameController?.Dispose();
                _inventoryController?.Dispose();
                break;

            case GameState.Rewards:
                _mainMenuController?.Dispose();
                var rewardView = ResourceLoader.LoadAndInstantiateView<RewardView>(_rewardView, _placeForUi);
                var saveDataRepository = new SaveDataRepository();
                saveDataRepository.Initialization();
                var currencyView = ResourceLoader.LoadAndInstantiateView<CurrencyWindow>(_currencyView, _placeForUi);
                var controller = new RewardController(rewardView, currencyView, saveDataRepository, _profilePlayer);
                break;

            case GameState.Garage:
                _inventoryModel = new InventoryModel();
                _inventoryController = new InventoryController(_upgradeItemConfigs, _abilityItemConfigs, _inventoryModel);
                _shedController = new ShedController(_upgradeItemConfigs, _profilePlayer, _inventoryModel, 
                                                        _inventoryController, _placeForUi);
                _shedController.Enter();
                _mainMenuController?.Dispose();
                break;

            case GameState.Game:

                if (_gameController != null)
                {
                    _shedController.Exit();
                    _shedController?.ChangeShedViewActiveState();
                    return;
                }

                foreach (var item in _inventoryController.ItemsRepository.ItemsMapBuID)
                {
                    if(item.Value is AbilityItem)
                        _inventoryModel.EquipItem(item.Value);
                }

                _shedController.Exit();
                _gameController = new GameController(_profilePlayer, _inventoryModel, _placeForUi, _shedController);
                _shedController?.ChangeShedViewActiveState();
                break;

            case GameState.Fight:
                _fightController = CreateFightController();
                break;
            default:
                AllClear();
                break;
        }
    }

    private FightController CreateFightController()
    {
        var fightView = ResourceLoader.LoadAndInstantiateView<FightWindowView>(_fightView, _placeForUi);
        return new FightController(fightView, _profilePlayer);
    }


    private void AllClear()
    {
        _inventoryController?.Dispose();
        _mainMenuController?.Dispose();
        _gameController?.Dispose();
    }
}
