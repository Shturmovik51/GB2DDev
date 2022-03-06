using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationController : BaseController
{
    private ProfilePlayer _profile;
    public LocalizationController(LocalizationView localizationView, ProfilePlayer profile)
    {
        _profile = profile;
        localizationView.StartCor(CloseLocalizationScreen);
        AddGameObjects(localizationView.gameObject);
    }

    private void CloseLocalizationScreen()
    {
        _profile.CurrentState.Value = Profile.GameState.Start;
    }

}
