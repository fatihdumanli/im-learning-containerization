using EventBus.Events;

namespace Catalog.API.IntegrationEvents
{
    public class OrderStockConfirmedIntegrationEvent : IntegrationEvent
    {
        public int OrderId { get; private set; }

        public OrderStockConfirmedIntegrationEvent(int orderId)
        {
            this.OrderId = orderId;            
        }
    }
}