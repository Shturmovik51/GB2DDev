using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model.Shop;

public class ShopController : BaseController
{
    private ProfilePlayer _profilePlayer;
    private ShopTools _shopTools;
    private MainMenuView _view;

    public ShopController(ProfilePlayer profilePlayer, ShopTools shopTools, MainMenuView view)
    {
        _profilePlayer = profilePlayer;
        _shopTools = shopTools;
        _view = view;
        _shopTools.OnSuccessPurchase.SubscribeOnChange(AddFuel);
        AddController(this);
    }

    protected override void OnDispose()
    {
        _shopTools.OnSuccessPurchase.UnSubscriptionOnChange(AddFuel);
        base.OnDispose();
    }

    private void AddFuel()
    {
        _profilePlayer.CurrentCar.AddFuel(100);
        _view.UpdateFuelCount(_profilePlayer.CurrentCar.Fuel);
    }
}
