using Tools;
using UnityEngine;

public class InputGameController : BaseController
{
    private BaseInputView _view;

    public InputGameController(SubscriptionProperty<float> leftMove, SubscriptionProperty<float> rightMove, Car car)
    {
        _view = LoadView();
        _view.Init(leftMove, rightMove, car.Speed);
        AddGameObjects(_view.gameObject);
    }

    private BaseInputView LoadView()
    {
        var handle = ResourceLoader.LoadAndInstantiatePrefab(ResourceReferences.StickControl, null);        
        AddAsyncHandle(handle);        
        return handle.Result.GetComponent<BaseInputView>();
    }
}

