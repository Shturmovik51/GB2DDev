using System;
using Tools;

public class RewardData 
{
    public SubscriptionProperty<int> Wood;
    public SubscriptionProperty<int> Diamond;
    public SubscriptionProperty<int> CurrentActiveDailySlot;
    public SubscriptionProperty<DateTime?> LastDailyRewardTime;
    public SubscriptionProperty<int> CurrentActiveWeeklySlot;
    public SubscriptionProperty<DateTime?> LastWeeklyRewardTime;

    public RewardData()
    {
        Wood = new PrefsSubscriptionProperty<int>(PrefsKeys.WoodKey, int.Parse);
        Diamond = new PrefsSubscriptionProperty<int>(PrefsKeys.DiamondKey, int.Parse);
        CurrentActiveDailySlot = new PrefsSubscriptionProperty<int>(PrefsKeys.ActiveDailySlotKey, int.Parse);
        LastDailyRewardTime = new PrefsSubscriptionProperty<DateTime?>(PrefsKeys.LastDayTimeKey, NullableDateTimeConverter());
        CurrentActiveWeeklySlot = new PrefsSubscriptionProperty<int>(PrefsKeys.ActiveWeeklySlotKey, int.Parse);
        LastWeeklyRewardTime = new PrefsSubscriptionProperty<DateTime?>(PrefsKeys.LastWeekTimeKey, NullableDateTimeConverter());
    }

    private static Func<string, DateTime?> NullableDateTimeConverter()
    {
        return (v) =>
        {
            if (DateTime.TryParse(v, out var value))
                return value;
            return null;
        };
    }
}