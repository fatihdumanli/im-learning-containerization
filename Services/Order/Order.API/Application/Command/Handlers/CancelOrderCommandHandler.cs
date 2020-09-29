using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.API.Infrastructure.Logging;
using Ordering.Domain.AggregatesModel.OrderAggregate;

namespace Ordering.API.Application.Command
{
    public class CancelOrderCommandHandler : Loggable, IRequestHandler<CancelOrderCommand, bool>
    {
        private IOrderRepository _orderRepository;
        public CancelOrderCommandHandler(IOrderRepository orderRepository,
            ILogger<Loggable> logger
        ) : base(logger)
        {
            this._orderRepository = orderRepository;            
        }
        public async Task<bool> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            var orderToCancel = await _orderRepository.GetAsync(orderId: request.OrderId);

            _logger.LogInformation(" [x] CancelOrderCommandHandler: Transitioning order status STOCKCONFIRMED ---> CANCELLED");
            orderToCancel.SetStatusCancelledWhenPaymentFailed();

            return await _orderRepository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}