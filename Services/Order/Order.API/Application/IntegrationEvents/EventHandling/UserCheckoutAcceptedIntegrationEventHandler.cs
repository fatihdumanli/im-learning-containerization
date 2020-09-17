using DomainDispatching;
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
        private DomainDispatcher _commandDispatcher;
        public UserCheckoutAcceptedIntegrationEventHandler(ILogger<UserCheckoutAcceptedIntegrationEventHandler> logger,
            DomainDispatcher cd)
        {
            this._logger = logger;
            this._commandDispatcher = cd;
        }

        public Task Handle(UserCheckoutAcceptedIntegrationEvent @event)
        {
            _logger.LogInformation(string.Format("[x] UserCheckoutAcceptedIntegrationEventHandler.Handle(): Received integration event: {0}",
                Newtonsoft.Json.JsonConvert.SerializeObject(@event)));
              
            //TODO: add payment fields to the CreateOrderCommand
            var command = new CreateOrderCommand(@event.UserId, @event.Street, @event.City, @event.State,
                @event.ZipCode, @event.Country, @event.Basket.Items);
                
            _commandDispatcher.DispatchCommand<CreateOrderCommand>(command);
                
           
            return null;
        }
    }
}