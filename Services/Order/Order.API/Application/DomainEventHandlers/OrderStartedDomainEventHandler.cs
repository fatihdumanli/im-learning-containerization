using System;
using DomainDispatching.DomainEvent;
using Microsoft.Extensions.Logging;
using Ordering.Domain.AggregatesModel.BuyerAggregate;
using Ordering.Domain.DomainEvents;

namespace Ordering.API.Application.DomainEventHandlers
{
    public class OrderStartedDomainEventHandler : IDomainEventHandler<OrderStartedDomainEvent>
    {
        private ILogger<OrderStartedDomainEventHandler> _logger;
        private IBuyerRepository _buyerRepository;
        public OrderStartedDomainEventHandler(IBuyerRepository repository, 
            ILogger<OrderStartedDomainEventHandler> logger)
        {
            _logger = logger;
            _buyerRepository = repository ?? throw new ArgumentNullException("OrderStartedDomainEventHandler needs an IBuyerRepository implementation.");
        }

        public void Handle(OrderStartedDomainEvent domainEvent)
        {
            _logger.LogInformation(" [x] OrderStartedDomainEventHandler.Handle(): Handling OrderStartedDomainEvent domain event...");

            var buyer = _buyerRepository.FindAsync(domainEvent.Buyer);


            
        }
    }
}