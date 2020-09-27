using EventBus.Events;

namespace Catalog.API.IntegrationEvents
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