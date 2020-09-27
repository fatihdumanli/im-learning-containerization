using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Domain.AggregatesModel.OrderAggregate;

namespace Ordering.API.Application.Command
{
    public class SetOrderStatusToStockConfirmedCommandHandler : IRequestHandler<SetOrderStatusToStockConfirmedCommand, bool>
    {
        private ILogger<SetOrderStatusToStockConfirmedCommandHandler> _logger;
        private IOrderRepository _orderRepository;

        public SetOrderStatusToStockConfirmedCommandHandler(
            IOrderRepository repository,
            ILogger<SetOrderStatusToStockConfirmedCommandHandler> logger)
        {
            _orderRepository = repository;
            _logger = logger;            
        }

        public async Task<bool> Handle(SetOrderStatusToStockConfirmedCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(" [x] SetOrderStatusToStockConfirmedCommandHandler: Handling the command for order with id {0}...", request.OrderId);
            
            var orderToUpdate = await _orderRepository.GetAsync(request.OrderId);

            //Domain layer
            //Adds a domain event called 'OrderStatusChangedToStockConfirmedDomainEvent'
            orderToUpdate.SetStatusStockConfirmed();
            
            _logger.LogInformation(" [x] SetOrderStatusToStockConfirmedCommandHandler: Transitioning order status AWAITING VALIDATION ---> STOCK CONFIRMED" +
                " for order with id {0}", request.OrderId);

            return await _orderRepository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}