using Tools;

public class GameController : BaseController
{
    public GameController(ProfilePlayer profilePlayer)
    {
        var leftMoveDiff = new SubscriptionProperty<float>();
        var rightMoveDiff = new SubscriptionProperty<float>();
        
        var tapeBackgroundController = new TapeBackgroundController(leftMoveDiff, rightMoveDiff);
        AddController(tapeBackgroundController);

        var carController = new CarController();
        AddController(carController);

        var fuelController = new FuelController(profilePlayer.CurrentCar.Fuel, carController.GetCarView());
        fuelController.UpdateView();
        AddController(fuelController);

        var inputGameController = new InputGameController(leftMoveDiff, rightMoveDiff, profilePlayer.CurrentCar, fuelController);
        AddController(inputGameController);
            

    }
}

