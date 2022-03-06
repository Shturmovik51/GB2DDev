﻿using System.Collections.Generic;
using Profile;
using UnityEngine;
using UnityEngine.Advertisements;

public class MainMenuController : BaseController
{
    private readonly ResourcePath _viewPath = new ResourcePath { PathResource = "Prefabs/MainMenu" };
    private readonly ProfilePlayer _profilePlayer;
    private readonly MainMenuView _view;

    public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer)
    {
        _profilePlayer = profilePlayer;
        _view = LoadView(placeForUi);
        AddGameObjects(_view.gameObject);
        _view.Init(StartGaRage, StartRewards, StartLocalization);
    }

    private MainMenuView LoadView(Transform placeForUi)
    {
        return ResourceLoader.LoadAndInstantiateView<MainMenuView>(_viewPath, placeForUi);
    }

    private void StartGaRage()
    {
        _profilePlayer.CurrentState.Value = GameState.Garage;

        _profilePlayer.AnalyticTools.SendMessage("start_game",
            new Dictionary<string, object>() { {"time", Time.realtimeSinceStartup } });
    }
    private void StartRewards()
    {
        _profilePlayer.CurrentState.Value = GameState.Rewards;

        _profilePlayer.AnalyticTools.SendMessage("start_rewards",
            new Dictionary<string, object>() { { "time", Time.realtimeSinceStartup } });
    }
    private void StartLocalization()
    {
        _profilePlayer.CurrentState.Value = GameState.Localization;

        _profilePlayer.AnalyticTools.SendMessage("start_localization",
            new Dictionary<string, object>() { { "time", Time.realtimeSinceStartup } });
    }

}

