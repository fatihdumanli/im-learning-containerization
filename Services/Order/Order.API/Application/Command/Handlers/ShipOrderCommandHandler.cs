using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Domain.AggregatesModel.OrderAggregate;

namespace Ordering.API.Application.Command
{
    public class ShipOrderCommandHandler : IRequestHandler<ShipOrderCommand, bool>
    {
        private ILogger<ShipOrderCommandHandler> _logger;
        private IOrderRepository _orderRepository;

        public ShipOrderCommandHandler(
            IOrderRepository orderRepository,
            ILogger<ShipOrderCommandHandler> logger)
        {
            this._orderRepository = orderRepository;
            this._logger = logger;
        }

        
        public async Task<bool> Handle(ShipOrderCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(" [x] ShipOrderCommandHandler: Shipping the order with id {0}", request.OrderId);

            var orderToShip = await _orderRepository.GetAsync(orderId: request.OrderId);

            _logger.LogInformation(" [X] ShipOrderCommandHandler: Transitioning order status PAID ----> SHIPPED");
            orderToShip.SetStatusShipped();            

            return await _orderRepository.UnitOfWork.SaveEntitiesAsync();;
        }
    }
}