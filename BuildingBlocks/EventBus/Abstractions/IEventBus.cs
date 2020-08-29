using System;
using System.Collections.Generic;
using System.Text;
using EventBus.Events;

namespace EventBus.Abstractions
{
    public interface IEventBus
    {
        
        void Publish(IntegrationEvent @event);    
        void Subscribe(string routingKey);
        void StartConsuming();
    }
}
