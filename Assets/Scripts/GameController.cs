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
        AddController(abilityRepository);

        var abilitiesController = CreateAbilitiesController(uiRoot, carController, inventoryModel, abilityRepository);
        AddController(abilitiesController);

        var pauseButtonObj = (GameObject)Object.Instantiate(Resources.Load("Prefabs/Game/PauseButton"), uiRoot);
        var pauseButton = pauseButtonObj.GetComponent<Button>();

        pauseButton.onClick.AddListener(() => shedController.ChangeShedViewActiveState(true));

        AddGameObjects(pauseButtonObj);

        var battlestartController = CreateBattleStartController(uiRoot, profilePlayer);
        AddController(battlestartController);
    }

    private AbilitiesController CreateAbilitiesController(Transform uiRoot, CarController carController, 
                                                    InventoryModel inventoryModel, AbilityRepository abilityRepository)
    {
        var abilityViewHandle = ResourceLoader.LoadAndInstantiatePrefab(ResourceReferences.AbilitiesView, uiRoot); 
        return new AbilitiesController(carController, inventoryModel, abilityRepository, abilityViewHandle);
    }

    private BattleStartController CreateBattleStartController(Transform uiRoot, ProfilePlayer profilePlayer)
    {
        var startViewHandle = ResourceLoader.LoadAndInstantiatePrefab(ResourceReferences.FightWindowView, uiRoot);        
        return new BattleStartController(startViewHandle, profilePlayer);
    }
}

