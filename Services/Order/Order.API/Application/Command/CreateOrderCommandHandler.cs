using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Ordering.Domain.AggregatesModel.OrderAggregate;

namespace Ordering.API.Application.Command
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, bool>
    {
        private ILogger<CreateOrderCommandHandler> _logger;
        private IOrderRepository _repository;
        public CreateOrderCommandHandler(IOrderRepository repository, 
            ILogger<CreateOrderCommandHandler> logger)
        {
            _logger = logger;
            _repository = repository;
        }

    

        public async Task<bool> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
               _logger.LogInformation(" [x] CreateOrderCommandHandler.Handle(): Handling the CreateOrderCommand: {0}", JsonConvert.SerializeObject(command));

            var address = new Address(street: command.Street, city: command.City, state: command.State,
                country: command.Country, zipCode: command.ZipCode);

            _logger.LogInformation(string.Format(" [x] CreateOrderCommandHandler.Handle(): Created 'Address' ValueObject: {0}",
                JsonConvert.SerializeObject(address)));

            var order = new Order(command.BuyerId, command.BuyerId, address, command.CardTypeId, command.CardNumber, command.Cvv,
                cardHolderName: command.CardHolderName, cardExpiration: command.CardExpiration);

            _logger.LogInformation(string.Format(" [x] CreateOrderCommandHandler.Handle(): Created 'Order' Entity: {0}",
                JsonConvert.SerializeObject(order)));

            foreach(var item in command.OrderItems)
            {
                order.AddOrderItem(item.ProductId, item.ProductName, item.UnitPrice, 0, string.Empty, item.Units);
            }

            _repository.Add(order);
            
            _logger.LogInformation(" [X] CreateOrderCommandHandler.Handle(): Order aggregate added to DbSet, calling SaveEntitiesAsync()...");
            _logger.LogInformation(" [X] CreateOrderCommandHandler.Handle(): Domain events to be published: " + JsonConvert.SerializeObject(order.DomainEvents));;

            return await _repository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}