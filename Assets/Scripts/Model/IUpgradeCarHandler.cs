public interface IUpgradeCarHandler
{
    IUpgradeableCar Upgrade(IUpgradeableCar car);
    IUpgradeableCar Degrade(IUpgradeableCar car);
}