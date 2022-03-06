using Tools;
using UnityEngine;

public class TapeBackgroundController : BaseController
{
    private TapeBackgroundView _view;
    private readonly SubscriptionProperty<float> _diff;
    private readonly IReadOnlySubscriptionProperty<float> _leftMove;
    private readonly IReadOnlySubscriptionProperty<float> _rightMove;

    public TapeBackgroundController(IReadOnlySubscriptionProperty<float> leftMove, 
        IReadOnlySubscriptionProperty<float> rightMove)
    {
        _view = LoadView();
        _diff = new SubscriptionProperty<float>();
        
        _leftMove = leftMove;
        _rightMove = rightMove;
        
        _view.Init(_diff);
        
        _leftMove.SubscribeOnChange(Move);
        _rightMove.SubscribeOnChange(Move);
    }
    
    protected override void OnDispose()
    {
        _leftMove.UnSubscriptionOnChange(Move);
        _rightMove.UnSubscriptionOnChange(Move);
        
        base.OnDispose();
    }

    private TapeBackgroundView LoadView()
    {
        var objViewHandle = ResourceLoader.LoadAndInstantiatePrefab(ResourceReferences.BackGround, null);
        AddAsyncHandle(objViewHandle);        
        return objViewHandle.Result.GetComponent<TapeBackgroundView>();
    }

    private void Move(float value)
    {
        _diff.Value = value;
    }
}

