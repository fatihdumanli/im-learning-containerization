using System.Threading.Tasks;
using EventBus.Abstractions;
using Ordering.API.Application.IntegrationEvents.Events;

namespace Ordering.API.Application.IntegrationEvents.EventHandling
{
    public class OrderStockConfirmedIntegrationEventHandler : IIntegrationEventHandler<OrderStockConfirmedIntegrationEvent>
    {
        public Task Handle(OrderStockConfirmedIntegrationEvent @event)
        {
            throw new System.NotImplementedException();
        }
    }
}