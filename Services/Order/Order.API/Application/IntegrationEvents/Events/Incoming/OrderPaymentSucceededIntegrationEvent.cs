using EventBus.Events;

namespace Ordering.API.Application.IntegrationEvents.Events
{
    public class OrderPaymentSucceededIntegrationEvent : IntegrationEvent
    {
        public int OrderId { get; private set; }

        public OrderPaymentSucceededIntegrationEvent(int orderId)
        {
            this.OrderId = orderId;            
        }
    }
}