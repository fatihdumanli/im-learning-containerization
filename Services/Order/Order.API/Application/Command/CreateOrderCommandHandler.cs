using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Ordering.Domain.AggregatesModel.OrderAggregate;

namespace Order.API.Application.Command
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, bool>
    {
        private ILogger<CreateOrderCommandHandler> _logger;
        private readonly IOrderRepository _orderRepository;

        public CreateOrderCommandHandler(IOrderRepository repository, ILogger<CreateOrderCommandHandler> logger)
        {
            this._orderRepository = repository;
            this._logger = logger;
        }
        /*
            1. Instantiate new Address valueObject
            2. Instantiate new Order aggregate root.
        */
        public async Task<bool> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {

            //In request,
            //--OrderItems.
            //--General information about order, and address
            
            var address = new Address(request.Street, request.City, request.State, request.Country, request.ZipCode);
            var order = new Ordering.Domain.AggregatesModel.OrderAggregate.Order(userId: request.UserId,
                userName: request.UserName, address: address,
                cardTypeId: request.CardTypeId, cardNumber: request.CardNumber,
                cardSecurityNumber: request.CardSecurityNumber, cardHolderName: request.CardHolderName,
                cardExpiration: request.CardExpiration);
            
            foreach(var item in request.OrderItems)
            {
                order.AddOrderItem(item.ProductId, item.ProductName, item.UnitPrice, item.Discount, item.PictureUrl, item.Units);
            }

            _logger.LogInformation("------- [.] Create Order Command Handler...");
            _logger.LogInformation("REQUEST --> " + JsonConvert.SerializeObject(request));
        
            _logger.LogInformation("----- Creating Order - Order: {@Order}", order);

            _orderRepository.Add(order);
                 
            _logger.LogInformation(" [x] CreateOrderCommandHandler: Order item is added to Order DbSet.");

            return await _orderRepository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}