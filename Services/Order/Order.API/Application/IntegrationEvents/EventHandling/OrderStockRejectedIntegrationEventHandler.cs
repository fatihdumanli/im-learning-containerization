using System.Threading.Tasks;
using EventBus.Abstractions;
using Ordering.API.Application.IntegrationEvents.Events;

namespace Ordering.API.Application.IntegrationEvents.EventHandling
{
    public class OrderStockRejectedIntegrationEventHandler : IIntegrationEventHandler<OrderStockRejectedIntegrationEvent>
    {
        public Task Handle(OrderStockRejectedIntegrationEvent @event)
        {
            throw new System.NotImplementedException();
        }
    }
}