using System;
using System.Collections.Generic;
using System.Text;
using EventBus.Events;

namespace EventBus.Abstractions
{
    public interface IEventBus
    {
        
        void Publish(IntegrationEvent @event);    
        void Subscribe<TE, TH>() where TE: IntegrationEvent 
                                 where TH: IIntegrationEventHandler<TE>;
        void StartConsuming();
    }
}
