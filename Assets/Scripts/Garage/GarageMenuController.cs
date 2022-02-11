using System.Collections;
using System.Collections.Generic;
using Profile;
using UnityEngine;

public class GarageMenuController : BaseController
{
    private readonly ResourcePath _viewPath = new ResourcePath { PathResource = "Prefabs/GarageMenu" };
    private readonly ProfilePlayer _profilePlayer;
    private readonly GarageMenuView _view;
    private bool _isFirstStart = true;

    public GarageMenuController(Transform placeForUi, ProfilePlayer profilePlayer)
    {
        _profilePlayer = profilePlayer;
        _view = LoadView(placeForUi);
        //AddGameObjects(_view.gameObject);
        _view.Init(StartGame);
    }

    private GarageMenuView LoadView(Transform placeForUi)
    {
        return ResourceLoader.LoadAndInstantiateView<GarageMenuView>(_viewPath, placeForUi);
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

    public void ChangeGarageViewActiveState()
    {
        _view.gameObject.SetActive(_view.gameObject.activeInHierarchy ? false : true);
    }
}
