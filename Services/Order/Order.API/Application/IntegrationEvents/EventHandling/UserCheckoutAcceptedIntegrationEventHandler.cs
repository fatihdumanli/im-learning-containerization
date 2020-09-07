using EventBus.Abstractions;
using Order.API.Application.IntegrationEvents.Events;
using System.Threading.Tasks;

namespace Order.API.Application.IntegrationEvents.EventHandling
{
    public class UserCheckoutAcceptedIntegrationEventHandler : IIntegrationEventHandler<UserCheckoutAcceptedIntegrationEvent>
    {
        public Task Handle(UserCheckoutAcceptedIntegrationEvent @event)
        {
            return null;
        }
    }
}