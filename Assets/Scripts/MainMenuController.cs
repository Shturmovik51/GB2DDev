using System.Collections.Generic;
using Model.Analytic;
using Profile;
using Tools.Ads;
using UnityEngine;
using Model.Shop;

public class MainMenuController : BaseController
{
    private readonly ResourcePath _viewPath = new ResourcePath {PathResource = "Prefabs/mainMenu"};
    private readonly ProfilePlayer _profilePlayer;
    private readonly IAnalyticTools _analytics;
    private readonly IAdsShower _ads;
    private readonly MainMenuView _view;

    private ShopController _shopController;
    private ShopTools _shopTools;

    public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer, IAnalyticTools analytics, IAdsShower ads)
    {
        _profilePlayer = profilePlayer;
        _analytics = analytics;
        _ads = ads;
        _view = LoadView(placeForUi);
        _view.Init(StartGame, profilePlayer.CurrentCar.Fuel);


        var products = new List<ShopProduct>()
        {
            new ShopProduct("com.c1.racing.fuel", UnityEngine.Purchasing.ProductType.NonConsumable),
        };

        _shopTools = new ShopTools(products);
        _shopController = new ShopController(_profilePlayer, _shopTools, _view);
    }

    private MainMenuView LoadView(Transform placeForUi)
    {
        var objectView = Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath), placeForUi, false);
        AddGameObjects(objectView);
        
        return objectView.GetComponent<MainMenuView>();
    }

    private void StartGame()
    {
        _analytics.SendMessage("Start", new Dictionary<string, object>());
        _ads.ShowInterstitial();
        _profilePlayer.CurrentState.Value = GameState.Game;
    }
}

