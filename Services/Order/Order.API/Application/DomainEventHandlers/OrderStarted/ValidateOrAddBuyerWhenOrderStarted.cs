using System;
using System.Threading;
using System.Threading.Tasks;
using DomainDispatching.DomainEvent;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Ordering.API.Infrastructure.Logging;
using Ordering.Domain.AggregatesModel.BuyerAggregate;
using Ordering.Domain.DomainEvents;
using Ordering.Domain.Exceptions;

namespace Ordering.API.Application.DomainEventHandlers
{
    public class ValidateOrAddBuyerWhenOrderStarted : Loggable, INotificationHandler<OrderStartedDomainEvent>
    {
        private IBuyerRepository _buyerRepository;
        public ValidateOrAddBuyerWhenOrderStarted(IBuyerRepository repository, 
            ILogger<Loggable> logger) : base(logger)
        {
            _buyerRepository = repository ?? throw new ArgumentNullException("OrderStartedDomainEventHandler needs an IBuyerRepository implementation.");
        }

        public async Task Handle(OrderStartedDomainEvent domainEvent, CancellationToken cancellationToken)
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


            try
            {
                 buyer.ValidatePaymentMethod(orderId: domainEvent.Order.Id, cardNumber: domainEvent.CardNumber, cardHolderName: domainEvent.CardHolderName,
                    cvv: domainEvent.Cvv, cardTypeId: domainEvent.CardTypeId, expiration: domainEvent.Expiration);   
            }

            catch(OrderDomainException e)
            {
                _logger.LogError(e.Message);
                throw e;
            }

            if(isBuyerExisted) 
            {
                _buyerRepository.Update(buyer);
            }  

            else 
            {
                _buyerRepository.Add(buyer);
            }
            
            _logger.LogInformation(" [x] OrderStartedDomainEventHandler.Handle(): Payment method is validated!");
            await _buyerRepository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}