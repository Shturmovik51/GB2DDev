using System.Collections.Generic;
using Model.Analytic;
using Profile;
using Tools.Ads;
using UnityEngine;

public class MainMenuController : BaseController
{
    private readonly ResourcePath _viewPath = new ResourcePath {PathResource = "Prefabs/mainMenu"};
    private readonly ProfilePlayer _profilePlayer;
    private readonly IAnalyticTools _analytics;
    private readonly IAdsShower _ads;
    private readonly MainMenuView _view;

    public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer, IAnalyticTools analytics, IAdsShower ads)
    {
        _profilePlayer = profilePlayer;
        _analytics = analytics;
        _ads = ads;
        _view = LoadView(placeForUi);
        _view.Init(StartGame, profilePlayer.CurrentCar.Fuel);
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

