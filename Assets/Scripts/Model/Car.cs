using UnityEngine;

public class Car : IUpgradeableCar
{
    public float Speed { get; set; }
    public float Acceleration { get; set; }

    private float _defaultSpeed;
    private float _defaultAcceleration = 0;

    public Car(float speed)
    {
        _defaultSpeed = speed;
        RestoreSpeed();
    }

    public void RestoreSpeed()
    {
        Speed = _defaultSpeed;
    }

    public void RestoreAcceleration()
    {
        Acceleration = _defaultAcceleration;
    }

}