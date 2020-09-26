using System;
using EventBus.Events;

namespace Billing.API.IntegrationEvents.Events
{
    public class GracePeriodConfirmedForOrderIntegrationEvent : IntegrationEvent
    {
        public int OrderId { get; private set; }
        public GracePeriodConfirmedForOrderIntegrationEvent(int orderId)
        {
            this.OrderId = orderId;            
        }
    }
}