using System;

namespace EventBus.Abstractions
{
    public interface IEventBusSubscriptionManager
    {
        Type GetHandlerType(string eventName);
        void AddSubscription<TE, TH>() where TE: Events.IntegrationEvent 
                                 where TH: IIntegrationEventHandler<TE>;
    }
}