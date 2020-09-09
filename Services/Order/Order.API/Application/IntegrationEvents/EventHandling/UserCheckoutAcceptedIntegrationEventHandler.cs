using EventBus.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;
using Order.API.Application.Command;
using Order.API.Application.IntegrationEvents.Events;
using System.Threading.Tasks;

namespace Order.API.Application.IntegrationEvents.EventHandling
{
    public class UserCheckoutAcceptedIntegrationEventHandler : IIntegrationEventHandler<UserCheckoutAcceptedIntegrationEvent>
    {
        private ILogger<UserCheckoutAcceptedIntegrationEventHandler> _logger;
        private IMediator _mediator;
        public UserCheckoutAcceptedIntegrationEventHandler(ILogger<UserCheckoutAcceptedIntegrationEventHandler> logger,
            IMediator mediator)
        {
            this._logger = logger;
            this._mediator = mediator;            
        }
        public Task Handle(UserCheckoutAcceptedIntegrationEvent @event)
        {
            _logger.LogInformation("\t [.] Inside the UserCheckoutAcceptedIntegrationEventHandler");

            //Burada CreateOrderCommand oluşturup, mediatr ile göndereceğiz.
            var command = new CreateOrderCommand();
            _mediator.Send(command);

            return null;
        }
    }
}