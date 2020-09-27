using System.Threading.Tasks;
using EventBus.Abstractions;
using Ordering.API.Application.IntegrationEvents.Events;

namespace Ordering.API.Application.IntegrationEvents.EventHandling
{
    public class OrderPaymentSucceededIntegrationEventHandler :
        IIntegrationEventHandler<OrderPaymentSucceededIntegrationEvent>
    {
        public Task Handle(OrderPaymentSucceededIntegrationEvent @event)
        {
            throw new System.NotImplementedException();
        }
    }
}