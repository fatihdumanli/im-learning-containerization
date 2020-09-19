using System;
using DomainDispatching.DomainEvent;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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
            _logger.LogInformation(" [x] OrderStartedDomainEventHandler.Handle(): Handling OrderStartedDomainEvent domain event: {0}",
                JsonConvert.SerializeObject(domainEvent));

            _logger.LogInformation(" [x] OrderStartedDomainEventHandler.Handle(): Looking for buyer: {0}", domainEvent.Buyer);
            Buyer buyer = _buyerRepository.FindByNameAsync(domainEvent.Buyer);

            var isBuyerExisted = buyer != null;
            if(isBuyerExisted)
            {
                _logger.LogInformation(" [x] OrderStartedDomainEventHandler.Handle(): Buyer found in persistance.", domainEvent.Buyer);
            }
            else 
            {
                buyer = new Buyer("fatih", "fatih");
            }        

            _logger.LogInformation(" [x] OrderStartedDomainEventHandler.Handle(): Payment method is being validated...");

            buyer.ValidatePaymentMethod(cardNumber: domainEvent.CardNumber, cardHolderName: domainEvent.CardHolderName,
                cvv: domainEvent.Cvv, cardTypeId: domainEvent.CardTypeId, expiration: domainEvent.Expiration);     

            _logger.LogInformation(" [x] OrderStartedDomainEventHandler.Handle(): Payment method is validated!");
        }
    }
}