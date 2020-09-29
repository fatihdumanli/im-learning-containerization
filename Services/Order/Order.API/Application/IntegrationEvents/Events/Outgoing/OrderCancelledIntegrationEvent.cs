using EventBus.Events;

namespace Ordering.API.Application.IntegrationEvents.Events
{
    public class OrderCancelledIntegrationEvent: IntegrationEvent
    {
        public int OrderId { get; private set; }

        public string CancellationReason { get; private set; }

        public OrderCancelledIntegrationEvent(int orderId, string reason)
        {
            this.OrderId = orderId;
            this.CancellationReason = reason;            
        }
    }
}