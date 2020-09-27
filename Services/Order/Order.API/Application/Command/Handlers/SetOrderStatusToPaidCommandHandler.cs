using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Domain.AggregatesModel.OrderAggregate;

namespace Ordering.API.Application.Command
{
    public class SetOrderStatusToPaidCommandHandler : IRequestHandler<SetOrderStatusToPaidCommand, bool>
    {
        private IOrderRepository _orderRepository;
        private ILogger<SetOrderStatusToPaidCommandHandler> _logger;

        public SetOrderStatusToPaidCommandHandler(
            IOrderRepository orderRepository,
            ILogger<SetOrderStatusToPaidCommandHandler> logger)
        {
            _logger = logger;
            _orderRepository = orderRepository;
        }

        public async Task<bool> Handle(SetOrderStatusToPaidCommand request, CancellationToken cancellationToken)
        {
            var orderPaid = await _orderRepository.GetAsync(orderId: request.OrderId);
            
            orderPaid.SetStatusToPaid();

            _logger.LogInformation(" [x] SetOrderStatusToPaidCommandHandler: Order status transitioned to PAID. Saving changes...");

            return await _orderRepository.UnitOfWork.SaveEntitiesAsync();            
        }
    }
}