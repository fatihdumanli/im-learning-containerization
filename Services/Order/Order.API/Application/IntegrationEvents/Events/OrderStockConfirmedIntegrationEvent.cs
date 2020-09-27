using EventBus.Events;

namespace Ordering.API.Application.IntegrationEvents.Events
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