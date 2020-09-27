using EventBus.Events;

namespace Ordering.API.Application.IntegrationEvents.Events
{
    public class OrderStockRejectedIntegrationEvent : IntegrationEvent
    {
        public int OrderId { get; private set; }

        public OrderStockRejectedIntegrationEvent(int orderId)
        {
            this.OrderId = orderId;
        }
    }
}