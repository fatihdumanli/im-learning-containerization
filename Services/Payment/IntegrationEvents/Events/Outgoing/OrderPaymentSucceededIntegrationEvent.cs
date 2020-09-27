using EventBus.Events;

namespace Payment.IntegrationEvents
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