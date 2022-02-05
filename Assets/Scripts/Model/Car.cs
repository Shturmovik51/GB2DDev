public class Car
{
    public float Speed { get;}
    public int Fuel { get; private set; }

    public Car(float speed, int fuel)
    {
        Speed = speed;
        Fuel = fuel;
    }

    public void AddFuel(int value)
    {
        Fuel += value;
    }
}

