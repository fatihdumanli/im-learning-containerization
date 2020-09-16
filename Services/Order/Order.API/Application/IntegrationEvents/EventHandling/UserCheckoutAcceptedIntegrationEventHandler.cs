using CommandDistpaching;
using EventBus.Abstractions;
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
        private CommandDispatcher _commandDispatcher;
        public UserCheckoutAcceptedIntegrationEventHandler(ILogger<UserCheckoutAcceptedIntegrationEventHandler> logger,
            CommandDispatcher cd)
        {
            this._logger = logger;
            this._commandDispatcher = cd;
        }

        public Task Handle(UserCheckoutAcceptedIntegrationEvent @event)
        {
            _logger.LogInformation("\t [.] Inside the UserCheckoutAcceptedIntegrationEventHandler");
            
        

            var command = new CreateOrderCommand(@event.Buyer, @event.Street, @event.City, @event.State,
                @event.ZipCode, @event.Country, @event.Basket.Items);
                
            _commandDispatcher.Dispatch<CreateOrderCommand>(command);
                
            
            /*
            var command = new CreateOrderCommand(@event.Basket.Items, userId: @event.UserId, userName: @event.UserName, city: @event.City,
                street: @event.Street, state: @event.State, country: @event.Country, zipCode: @event.ZipCode,
                cardNumber: @event.CardNumber, cardHolderName: @event.CardHolderName, cardExpiration: @event.CardExpiration,
                cardSecurityNumber: @event.CardSecurityNumber, cardTypeId: @event.CardTypeId);
                
            */
            //TODO publish command
            return null;
        }
    }
}