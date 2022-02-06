using Tools;
using UnityEngine;

public abstract class BaseInputView : MonoBehaviour
{
    private SubscriptionProperty<float> _leftMove;
    private SubscriptionProperty<float> _rightMove;
    private FuelController _fuelController;
    private float _stepCount = 0f;
    private float _fuelConsumptionInterval = 4f;


    protected float _speed;
    
    public virtual void Init(SubscriptionProperty<float> leftMove, SubscriptionProperty<float> rightMove, float speed, FuelController fuelCountController)
    {
        _leftMove = leftMove;
        _rightMove = rightMove;
        _speed = speed;
        _fuelController = fuelCountController;
    }
    
    protected void OnLeftMove(float value)
    {
        if (FuelCheck()) return;

        _leftMove.Value = value;
        FuelChange(value);
    }

    protected void OnRightMove(float value)
    {
        if (FuelCheck()) return;

        _rightMove.Value = value;
        FuelChange(value);
    }

    protected bool FuelCheck()
    {
        return (_fuelController.FuelCount <= 0);           
    }

    protected void FuelChange(float step)
    {
        if (step != 0)
            _fuelController.RotateGarWheels(-step * Time.deltaTime);

        _stepCount += step;

        if (Mathf.Abs(_stepCount) > _fuelConsumptionInterval)
        {
            if (_stepCount > 0)
                _stepCount -= _fuelConsumptionInterval;
            if (_stepCount < 0)
                _stepCount += _fuelConsumptionInterval;

            _fuelController.DecreaseFuel();
        }
    }
}

