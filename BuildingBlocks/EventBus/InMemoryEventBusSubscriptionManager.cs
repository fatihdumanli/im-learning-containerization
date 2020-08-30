using System;
using System.Collections.Generic;
using System.Linq;
using EventBus.Abstractions;
using EventBus.Events;
using Microsoft.Extensions.Logging;

namespace EventBus
{
    public class InMemoryEventBusSubscriptionManager : IEventBusSubscriptionManager
    {   
        private List<EventBusSubscription> _subscriptions;
        private ILogger<InMemoryEventBusSubscriptionManager> _logger;

        public InMemoryEventBusSubscriptionManager(ILogger<InMemoryEventBusSubscriptionManager> logger)
        {   
            _logger = logger;

            if(this._subscriptions == null)
                this._subscriptions = new List<EventBusSubscription>();
        }

        public Type GetHandlerType(string eventName)
        {
            
            return null;     
        }

        public void AddSubscription<TE, TH>()
            where TE : IntegrationEvent
            where TH : IIntegrationEventHandler<TE>
        {
            this._subscriptions.Add(new EventBusSubscription(typeof(TE), typeof(TH)));

            _logger.LogInformation(" [x] Log from InMemoryEventBusSubsriptionManager." +
            "AddSubscription<TE, TH>()");

            _logger.LogInformation(" \t [.] {0} - {1} added to subscription collection.",
                typeof(TE).GetType().Name, typeof(TH).GetType().Name);

            _logger.LogInformation(" \t\t Subscription list:");

            foreach(var subInfo in this._subscriptions)
            {
                _logger.LogInformation(" \t\t\t Event: {0}, Handler: {1}", subInfo.Event, 
                    subInfo.EventHandlerType);
            }

        }
    }

}