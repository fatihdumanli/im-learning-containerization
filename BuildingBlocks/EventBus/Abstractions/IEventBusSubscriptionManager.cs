using System;

namespace EventBus.Abstractions
{
    public interface IEventBusSubscriptionManager
    {
        bool HasSubscription(string eventName);
        Type GetHandlerType(string eventName);
        Type GetEventType(string eventName);
        void AddSubscription<TE, TH>() where TE: Events.IntegrationEvent 
                                 where TH: IIntegrationEventHandler<TE>;
    }
}