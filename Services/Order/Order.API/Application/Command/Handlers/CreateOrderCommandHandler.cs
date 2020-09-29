using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Ordering.API.Infrastructure.Logging;
using Ordering.Domain.AggregatesModel.OrderAggregate;

namespace Ordering.API.Application.Command
{
    public class CreateOrderCommandHandler : Loggable, IRequestHandler<CreateOrderCommand, bool>
    {
        private IOrderRepository _repository;

        public CreateOrderCommandHandler(IOrderRepository repository,
            ILogger<Loggable> logger)
            : base(logger)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation(" [x] CreateOrderCommandHandler.Handle(): Handling the CreateOrderCommand.");

            var address = new Address(street: command.Street, city: command.City, state: command.State,
                country: command.Country, zipCode: command.ZipCode);

            _logger.LogInformation(string.Format(" [x] CreateOrderCommandHandler.Handle(): Created 'Address' Entity."));

            var order = new Order(command.BuyerId, command.BuyerId, address, command.CardTypeId, command.CardNumber, command.Cvv,
                cardHolderName: command.CardHolderName, cardExpiration: command.CardExpiration);

            _logger.LogInformation(string.Format(" [x] CreateOrderCommandHandler.Handle(): Created 'Order' Entity."));

            foreach(var item in command.OrderItems)
            {
                order.AddOrderItem(item.ProductId, item.ProductName, item.UnitPrice, 0, string.Empty, item.Units);
            }

            try
            {
                _repository.Add(order);
            } 
            catch(Exception e)
            {
                _logger.LogError(e.Message);
            }

            
            _logger.LogInformation(" [X] CreateOrderCommandHandler.Handle(): Order aggregate added to DbSet, calling SaveEntitiesAsync()...");

            return await _repository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}