using System;

namespace Tools
{
    public interface IReadOnlySubscriptionAction
    {
        void SubscribeOnChange(Action<string> subscriptionAction);
        void UnSubscriptionOnChange(Action<string> unsubscriptionAction);
    }
}