using UnityEngine;

public class Car : IUpgradeableCar
{
    public float Speed { get; set; }
    public float Axeleration { get; set; }

    private float _defaultSpeed;
    private float _defaultAxeleration = 0;

    public Car(float speed)
    {
        _defaultSpeed = speed;
        RestoreSpeed();
    }

    public void RestoreSpeed()
    {
        Speed = _defaultSpeed;
    }

    public void RestoreAxeleration()
    {
        Axeleration = _defaultAxeleration;
    }

}