using System;
using DomainDispatching.DomainEvent;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Ordering.Domain.AggregatesModel.BuyerAggregate;
using Ordering.Domain.DomainEvents;

namespace Ordering.API.Application.DomainEventHandlers
{
    public class ValidateOrAddBuyerWhenOrderStarted : IDomainEventHandler<OrderStartedDomainEvent>
    {
        private ILogger<ValidateOrAddBuyerWhenOrderStarted> _logger;
        private IBuyerRepository _buyerRepository;
        public ValidateOrAddBuyerWhenOrderStarted(IBuyerRepository repository, 
            ILogger<ValidateOrAddBuyerWhenOrderStarted> logger)
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
                _logger.LogInformation(" [x] OrderStartedDomainEventHandler.Handle(): Buyer NOT found in persistance. Creating new Buyer instance...", domainEvent.Buyer);
                buyer = new Buyer(domainEvent.Buyer, domainEvent.Buyer);
            }        

            _logger.LogInformation(" [x] OrderStartedDomainEventHandler.Handle(): Payment method is being validated...");

            buyer.ValidatePaymentMethod(cardNumber: domainEvent.CardNumber, cardHolderName: domainEvent.CardHolderName,
                cvv: domainEvent.Cvv, cardTypeId: domainEvent.CardTypeId, expiration: domainEvent.Expiration);   


            if(isBuyerExisted) 
            {
                _buyerRepository.Update(buyer);
            }  

            else 
            {
                _buyerRepository.Add(buyer);
            }
            
            _logger.LogInformation(" [x] OrderStartedDomainEventHandler.Handle(): Payment method is validated!");
            _buyerRepository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}