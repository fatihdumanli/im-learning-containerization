using System;

namespace EventBus
{
    public class EventBusSubscription
    {
        public Type Event { get; set; }
        public Type EventHandlerType { get; set; }

        public EventBusSubscription(Type @event, Type eventHandlerType)
        {
            this.Event = @event;
            this.EventHandlerType = eventHandlerType;
        }
    }
}