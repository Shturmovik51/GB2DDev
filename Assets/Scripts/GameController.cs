using System.Collections.Generic;
using Features.AbilitiesFeature;
using Tools;
using UnityEngine;
using UnityEngine.UI;

public class GameController : BaseController
{
    public GameController(ProfilePlayer profilePlayer, IReadOnlyList<AbilityItemConfig> configs, InventoryModel inventoryModel,
                            Transform uiRoot, GarageMenuController garageMenuController)
    {
        var leftMoveDiff = new SubscriptionProperty<float>();
        var rightMoveDiff = new SubscriptionProperty<float>();
        
        var tapeBackgroundController = new TapeBackgroundController(leftMoveDiff, rightMoveDiff);
        AddController(tapeBackgroundController);
        
        var inputGameController = new InputGameController(leftMoveDiff, rightMoveDiff, profilePlayer.CurrentCar);
        AddController(inputGameController);
            
        var carController = new CarController();
        AddController(carController);

        var abilityRepository = new AbilityRepository(configs);
        var abilityView =
            ResourceLoader.LoadAndInstantiateView<AbilitiesView>(
                new ResourcePath() { PathResource = "Prefabs/AbilitiesView" }, uiRoot);
        var abilitiesController = new AbilitiesController(carController, inventoryModel, abilityRepository,
            abilityView);
        AddController(abilitiesController);

        var pauseButtonObj = (GameObject)Object.Instantiate(Resources.Load("Prefabs/PauseButton"), uiRoot);
        var pauseButton = pauseButtonObj.GetComponent<Button>();

        pauseButton.onClick.AddListener(garageMenuController.ChangeGarageViewActiveState);

        AddGameObjects(pauseButtonObj);
    }
}

