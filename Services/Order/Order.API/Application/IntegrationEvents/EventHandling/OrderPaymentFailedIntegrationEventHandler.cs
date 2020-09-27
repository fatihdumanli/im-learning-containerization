using System.Threading.Tasks;
using EventBus.Abstractions;
using Ordering.API.Application.IntegrationEvents.Events;

namespace Ordering.API.Application.IntegrationEvents.EventHandling
{
    public class OrderPaymentFailedIntegrationEventHandler
        : IIntegrationEventHandler<OrderPaymentFailedIntegrationEvent>
    {
        public Task Handle(OrderPaymentFailedIntegrationEvent @event)
        {
            throw new System.NotImplementedException();
        }
    }
}