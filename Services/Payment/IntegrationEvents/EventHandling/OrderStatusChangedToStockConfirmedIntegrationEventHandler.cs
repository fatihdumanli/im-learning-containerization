using System.Threading.Tasks;
using EventBus.Abstractions;

namespace Payment.IntegrationEvents
{
    public class OrderStatusChangedToStockConfirmedIntegrationEventHandler :
        IIntegrationEventHandler<OrderStatusChangedToStockConfirmedIntegrationEvent>
    {
        public Task Handle(OrderStatusChangedToStockConfirmedIntegrationEvent @event)
        {
            throw new System.NotImplementedException();
        }
    }
}