using EventBus.Events;

namespace Ordering.API.Application.IntegrationEvents.Events
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