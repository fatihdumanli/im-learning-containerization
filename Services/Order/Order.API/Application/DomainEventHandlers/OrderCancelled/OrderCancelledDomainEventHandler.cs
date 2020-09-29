using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.API.Application.IntegrationEvents.Events;
using Ordering.API.Application.IntegrationEvents.IntegrationEventService;
using Ordering.Domain.DomainEvents;
using Ordering.Domain.SharedKernel;

namespace Ordering.API.Application.DomainEventHandlers
{
    public class OrderCancelledDomainEventHandler : IDomainEventHandler<OrderCancelledDomainEvent>
    {
        private ILogger<OrderCancelledDomainEventHandler> _logger;
        private IOrderingIntegrationEventService _orderingIntegrationEventService;
    
        public OrderCancelledDomainEventHandler(
            IOrderingIntegrationEventService orderingIntegrationEventService,
            ILogger<OrderCancelledDomainEventHandler> logger)
        {
            _orderingIntegrationEventService = orderingIntegrationEventService;
            _logger = logger;
        }
        public async Task Handle(OrderCancelledDomainEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(" [x] OrderCancelledDomainEventHandler: Handling domain event...");

            var integrationEvent = new OrderCancelledIntegrationEvent(orderId: notification.OrderId, reason: notification.CancellationReason);

            _logger.LogInformation(" [x] OrderCancelledDomainEventHandler: Publishing integration event OrderCancelledIntegrationEvent");
            await _orderingIntegrationEventService.PublishEvent(integrationEvent);
        }
    }
}