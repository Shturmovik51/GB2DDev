using System.Collections.Generic;
using Profile;
using UnityEngine;

public class ShedController : BaseController, IShedController
{
    private readonly IReadOnlyList<UpgradeItemConfig> _upgradeItems;
    private readonly Car _car;
    private readonly UpgradeHandlerRepository _upgradeRepository;
    private readonly InventoryController _inventoryController;
    private readonly InventoryModel _inventoryModel;

    private readonly ResourcePath _viewPath = new ResourcePath { PathResource = "Prefabs/GarageMenu" };
    private readonly ProfilePlayer _profilePlayer;
    private readonly ShedView _view;
    private bool _isFirstStart = true;



    public ShedController(IReadOnlyList<UpgradeItemConfig> upgradeItems, List<ItemConfig> items, ProfilePlayer profilePlayer,
                            InventoryModel inventoryModel, InventoryController inventoryController, Transform placeForUi)
    {
        _upgradeItems = upgradeItems;
        _car = profilePlayer.CurrentCar;

        _upgradeRepository = new UpgradeHandlerRepository(upgradeItems);
        AddController(_upgradeRepository);

        
        _inventoryController = inventoryController;
        _inventoryModel = inventoryModel;

         _profilePlayer = profilePlayer;
        _view = LoadView(placeForUi);
        //AddGameObjects(_view.gameObject);
        _view.Init(StartGame);


    }

    public void Enter()
    {
        _inventoryController.ShowInventory();
        Debug.Log($"Enter, car speed = {_car.Speed}");
    }

    public void Exit()
    {
        UpgradeCarWithEquipedItems(_car, _inventoryModel.GetEquippedItems(), _upgradeRepository.Content);
        Debug.Log($"Exit, car speed = {_car.Speed}");
    }

    private void UpgradeCarWithEquipedItems(IUpgradeableCar car,
        IReadOnlyList<IItem> equiped,
        IReadOnlyDictionary<int, IUpgradeCarHandler> upgradeHandlers)
    {
        foreach (var item in equiped)
        {
            if (upgradeHandlers.TryGetValue(item.Id, out var handler))
                handler.Upgrade(car);
        }
    }





    private ShedView LoadView(Transform placeForUi)
    {
        return ResourceLoader.LoadAndInstantiateView<ShedView>(_viewPath, placeForUi);
    }

    private void StartGame()
    {
        if (_isFirstStart)
        {
            _view.SetButtonTextAsContinue();
            _isFirstStart = false;
        }

        _profilePlayer.CurrentState.Value = GameState.Game;

        _profilePlayer.AnalyticTools.SendMessage("start_game",
            new Dictionary<string, object>() { { "time", Time.realtimeSinceStartup } });
    }

    public void ChangeShedViewActiveState()
    {
        _view.gameObject.SetActive(_view.gameObject.activeInHierarchy ? false : true);
    }
}