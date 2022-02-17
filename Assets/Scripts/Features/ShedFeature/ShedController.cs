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
    public ShedController(IReadOnlyList<UpgradeItemConfig> upgradeItems, ProfilePlayer profilePlayer,
                            InventoryModel inventoryModel, InventoryController inventoryController, Transform placeForUi)
    {
        _upgradeItems = upgradeItems;
        _car = profilePlayer.CurrentCar;

        _upgradeRepository = new UpgradeHandlerRepository(upgradeItems);
        AddController(_upgradeRepository);

        _view = LoadView(placeForUi);      
        _view.Init(StartGame);
        
        _inventoryController = inventoryController;
        _inventoryController.InitInventoryView(_view.PlaceForShedInventory);
        _inventoryModel = inventoryModel;

         _profilePlayer = profilePlayer;
    }

    public void Enter()
    {
        _inventoryController.ShowInventory();
        Debug.Log($"Enter, car speed = {_car.Speed}");
    }

    public void Exit()
    {
        UpgradeCarWithEquipedItems(_car, _inventoryModel.GetEquippedItems(), _upgradeRepository.ItemsMapBuID);
        Debug.Log($"Exit, car speed = {_car.Speed}");
    }

    private void UpgradeCarWithEquipedItems(IUpgradeableCar car, IReadOnlyList<IItem> equiped, 
                                            IReadOnlyDictionary<int, IUpgradeCarHandler> handlerMap)
    {
        foreach (var item in equiped)
        {            
            var itemproperty = item.GetItemProperty<UpgradeItem>();
            if(itemproperty != null)
            {
                if (itemproperty.IsActive && itemproperty.IsEquipped)
                    continue;

                if (handlerMap.TryGetValue(item.ItemID, out var handler))
                {                
                    if (itemproperty.IsActive && !itemproperty.IsEquipped)
                    {
                        handler.Upgrade(car);
                        itemproperty.ChangeItemEquippedStatus(true);
                    }
                    if(!itemproperty.IsActive && itemproperty.IsEquipped)
                    {
                        handler.Degrade(car);
                        itemproperty.ChangeItemEquippedStatus(false);
                    }                
                }
            }
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