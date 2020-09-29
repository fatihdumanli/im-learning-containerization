using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.API.Application.IntegrationEvents.Events;
using Ordering.API.Application.IntegrationEvents.IntegrationEventService;
using Ordering.Domain.DomainEvents;

namespace Ordering.API.Application.DomainEventHandlers
{
    public class OrderShippedDomainEventHandler : INotificationHandler<OrderShippedDomainEvent>
    {
        private ILogger<OrderShippedDomainEventHandler> _logger;
        private IOrderingIntegrationEventService _orderingIntegrationEventService;
        
        public OrderShippedDomainEventHandler(IOrderingIntegrationEventService integrationEventService,
            ILogger<OrderShippedDomainEventHandler> logger)
        {
            this._orderingIntegrationEventService = integrationEventService;
            this._logger = logger;            
        }
        public async Task Handle(OrderShippedDomainEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(" [x] OrderShippedDomainEventHandler: Handling domain event...");

            var integrationEvent = new OrderShippedIntegrationEvent(orderId: notification.OrderId);
            
            _logger.LogInformation(" [x] OrderShippedDomainEventHandler: Publishing OrderShippedIntegrationEvent integration event...");
            await _orderingIntegrationEventService.PublishEvent(integrationEvent);
        }
    }
}