using System.Collections.Generic;
using Features.AbilitiesFeature;
using Tools;
using UnityEngine;
using UnityEngine.UI;

public class GameController : BaseController
{
    public GameController(ProfilePlayer profilePlayer, InventoryModel inventoryModel,
                            Transform uiRoot, ShedController shedController)
    {
        var leftMoveDiff = new SubscriptionProperty<float>();
        var rightMoveDiff = new SubscriptionProperty<float>();
        
        var tapeBackgroundController = new TapeBackgroundController(leftMoveDiff, rightMoveDiff);
        AddController(tapeBackgroundController);
        
        var inputGameController = new InputGameController(leftMoveDiff, rightMoveDiff, profilePlayer.CurrentCar);
        AddController(inputGameController);
            
        var carController = new CarController();
        AddController(carController);

        var abilityRepository = new AbilityRepository(inventoryModel.GetEquippedItems());
        var abilityView =
            ResourceLoader.LoadAndInstantiateView<AbilitiesView>(
                new ResourcePath() { PathResource = "Prefabs/AbilitiesView" }, uiRoot);
        var abilitiesController = new AbilitiesController(carController, inventoryModel, abilityRepository, abilityView);
        AddController(abilitiesController);

        var pauseButtonObj = (GameObject)Object.Instantiate(Resources.Load("Prefabs/PauseButton"), uiRoot);
        var pauseButton = pauseButtonObj.GetComponent<Button>();

        pauseButton.onClick.AddListener(shedController.ChangeShedViewActiveState);

        AddGameObjects(pauseButtonObj);
    }
}

