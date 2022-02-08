using Profile;
using Tools;

public class ProfilePlayer
{
    public ProfilePlayer(float speedCar, int fuel)
    {
        CurrentState = new SubscriptionProperty<GameState>();
        CurrentCar = new Car(speedCar, fuel);
    }

    public SubscriptionProperty<GameState> CurrentState { get; }

    public Car CurrentCar { get; }
}

