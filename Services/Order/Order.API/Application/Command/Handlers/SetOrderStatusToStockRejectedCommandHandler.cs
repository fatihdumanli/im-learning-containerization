using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Domain.AggregatesModel.OrderAggregate;

namespace Ordering.API.Application.Command
{
    public class SetOrderStatusToStockRejectedCommandHandler : IRequestHandler<SetOrderStatusToStockRejectedCommand, bool>
    {
        private ILogger<SetOrderStatusToStockRejectedCommandHandler> _logger;
        private IOrderRepository _orderRepository;

        public SetOrderStatusToStockRejectedCommandHandler(IOrderRepository orderRepository,
            ILogger<SetOrderStatusToStockRejectedCommandHandler> logger)
        {
            this._orderRepository = orderRepository;
            this._logger = logger;
        }
        
        public async Task<bool> Handle(SetOrderStatusToStockRejectedCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(" [x] SetOrderStatusToStockRejectedCommandHandler: Handling the command...");

            var orderToUpdate = await _orderRepository.GetAsync(orderId: request.OrderId);

            orderToUpdate.SetStatusCancelledWhenStockRejected();
            _logger.LogInformation(" [x] SetOrderStatusToStockRejectedCommandHandler: Transitioning order status AWAITING VALIDATION ---> CANCELLED" +
                " for order with id {0}", request.OrderId);

            return await _orderRepository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}