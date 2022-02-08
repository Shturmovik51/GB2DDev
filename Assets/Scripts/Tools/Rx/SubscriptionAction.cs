using System;

namespace Tools
{
    public class SubscriptionAction : IReadOnlySubscriptionAction
    {
        private Action<string> _action;

        public void Invoke(string iD)
        {
            _action?.Invoke(iD);
        }

        public void SubscribeOnChange(Action<string> subscriptionAction)
        {
            _action += subscriptionAction;
        }

        public void UnSubscriptionOnChange(Action<string> unsubscriptionAction)
        {
            _action -= unsubscriptionAction;
        }
    }
}