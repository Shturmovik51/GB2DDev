using System.Collections.Generic;
using System.Linq;
using Model.Analytic;
using Profile;
using Tools.Ads;
using UnityEngine;

public class Root : MonoBehaviour
{
    [SerializeField] private Transform _placeForUi;
    [SerializeField] private UnityAdsTools _ads;

    private MainController _mainController;
    private IAnalyticTools _analyticsTools;

    private void Awake()
    {
        _analyticsTools = new UnityAnalyticTools();
        var profilePlayer = new ProfilePlayer(15f, _ads, _analyticsTools);
        _mainController = new MainController(_placeForUi, profilePlayer);        
        profilePlayer.CurrentState.Value = GameState.Start;
    }

    protected void OnDestroy()
    {
        _mainController?.Dispose();
    }
}
