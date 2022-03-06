using UnityEngine;

public class CarController : BaseController, IAbilityActivator
{
    private readonly CarView _carView;

    public CarController()
    {
        _carView = LoadView();
    }

    private CarView LoadView()
    {
        var objView = ResourceLoader.LoadAndInstantiatePrefab(ResourceReferences.Car, null);        
        AddAsyncHandle(objView);        
        return objView.Result.GetComponent<CarView>();
    }

    public GameObject GetViewObject()
    {
        return _carView.gameObject;
    }
}

