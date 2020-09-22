using DomainDispatching;
using EventBus.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.API.Application.Command;
using Ordering.API.Application.IntegrationEvents.Events;
using Ordering.Domain.AggregatesModel.OrderAggregate;
using System.Threading.Tasks;

namespace Ordering.API.Application.IntegrationEvents.EventHandling
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

        public async Task Handle(UserCheckoutAcceptedIntegrationEvent @event)
        {
            _logger.LogInformation(string.Format("[x] UserCheckoutAcceptedIntegrationEventHandler.Handle(): Received integration event: {0}",
                Newtonsoft.Json.JsonConvert.SerializeObject(@event)));
              
            //TODO: add payment fields to the CreateOrderCommand
            var command = new CreateOrderCommand(@event.UserId, @event.Street, @event.City, @event.State,
                @event.ZipCode, @event.Country, cardNumber: @event.CardNumber, cardHolderName: @event.CardHolderName,
                cvv: @event.CardSecurityNumber, expiration: @event.CardExpiration, cardTypeId: @event.CardTypeId, @event.Basket.Items);
                
            await _mediator.Send(command);

        }
    }
}