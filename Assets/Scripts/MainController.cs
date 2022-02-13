using Profile;
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
    private readonly List<ItemConfig> _itemsConfig;
    private readonly IReadOnlyList<UpgradeItemConfig> _upgradeItems;
    private readonly List<AbilityItem> _abilityItems;


    private InventoryModel _inventoryModel;

    public MainController(Transform placeForUi, ProfilePlayer profilePlayer,
        IReadOnlyList<UpgradeItemConfig> upgradeItems,
        IReadOnlyList<AbilityItemConfig> abilityItems)
    {
        _profilePlayer = profilePlayer;
        _placeForUi = placeForUi;

        _upgradeItems = upgradeItems;
                
        _abilityItems = new List<AbilityItem>();
        foreach (var item in abilityItems)
        {
            var ability = new AbilityItem(item.Item.Id, item.View, item.Type, item.Sprite, item.Value, item.Duration);
            _abilityItems.Add(ability);
        }

        //var itemsSource =
        //    ResourceLoader.LoadDataSource<ItemConfig>(new ResourcePath()
        //        { PathResource = "Data/ItemsSource" });
        //_itemsConfig = itemsSource.Content.ToList();

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

            case GameState.Garage:
                _inventoryModel = new InventoryModel();
                _inventoryController = new InventoryController(_upgradeItems, _inventoryModel);
                _shedController = new ShedController(_upgradeItems, _itemsConfig, _profilePlayer, _inventoryModel, 
                                                        _inventoryController, _placeForUi);
                _shedController.Enter();
                //_inventoryController.ShowInventory();
                _mainMenuController?.Dispose();
                break;

            case GameState.Game:

                if (_gameController != null)
                {
                    _shedController.Exit();
                    _shedController?.ChangeShedViewActiveState();
                    return;
                }


                foreach (var ability in _abilityItems)
                {
                    _inventoryModel.EquipAbility(ability);
                }
                _shedController.Exit();
                _gameController = new GameController(_profilePlayer, _abilityItems.AsReadOnly(), _inventoryModel, _placeForUi, _shedController);
                _shedController?.ChangeShedViewActiveState();

                break;
            default:
                AllClear();
                break;
        }
    }

    private void AllClear()
    {
        _inventoryController?.Dispose();
        _mainMenuController?.Dispose();
        _gameController?.Dispose();
    }
}
