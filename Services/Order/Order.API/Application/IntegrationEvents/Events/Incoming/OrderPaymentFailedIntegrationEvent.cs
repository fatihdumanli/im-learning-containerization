using EventBus.Events;

namespace Ordering.API.Application.IntegrationEvents.Events
{
    public class OrderPaymentFailedIntegrationEvent : IntegrationEvent
    {
        public int OrderId { get; private set; }
        public string Reason { get; private set; }

        public OrderPaymentFailedIntegrationEvent(int orderId, string reason)
        {
            this.OrderId = orderId;
            this.Reason = reason;            
        }
    }
}