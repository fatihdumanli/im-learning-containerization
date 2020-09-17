using DomainDispatching.DomainEvent;
using Microsoft.Extensions.Logging;
using Ordering.Domain.DomainEvents;

namespace Ordering.API.Application.DomainEventHandlers
{
    public class OrderStartedDomainEventHandler : IDomainEventHandler<OrderStartedDomainEvent>
    {
        private ILogger<OrderStartedDomainEventHandler> _logger;
        
        public OrderStartedDomainEventHandler(ILogger<OrderStartedDomainEventHandler> logger)
        {
            _logger = logger;
        }
        public void Handle(OrderStartedDomainEvent domainEvent)
        {
            _logger.LogInformation(" [x] OrderStartedDomainEventHandler.Handle(): Handling OrderStartedDomainEvent domain event...");
        }
    }
}