using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model.Shop;

public class ShopController : BaseController
{
    private ProfilePlayer _profilePlayer;
    private ShopTools _shopTools;
    private MainMenuView _view;
    private List<ShopProduct> _products;

    public ShopController(ProfilePlayer profilePlayer, ShopTools shopTools, MainMenuView view, List<ShopProduct> products)
    {
        _profilePlayer = profilePlayer;
        _shopTools = shopTools;
        _view = view;
        _products = products;
        _shopTools.OnSuccessPurchase.SubscribeOnChange(AddProduct);
        AddController(this);
    }

    protected override void OnDispose()
    {
        _shopTools.OnSuccessPurchase.UnSubscriptionOnChange(AddProduct);
        base.OnDispose();
    }

    private void AddProduct(string iD)
    {
        var tempProduct = _products.Find(product => product.Id == iD);

        if (tempProduct == null) return;

        switch (tempProduct.Type)
        {
            case TypeOfProduct.Fuel:
                _profilePlayer.CurrentCar.AddFuel(tempProduct.Count);
                _view.UpdateFuelCount(_profilePlayer.CurrentCar.Fuel);
                break;
            case TypeOfProduct.Gold:
                break;
            default:
                break;
        }
    }
}
