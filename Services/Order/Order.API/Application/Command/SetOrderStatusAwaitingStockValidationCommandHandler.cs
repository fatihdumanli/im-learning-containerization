using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Domain.AggregatesModel.OrderAggregate;

namespace Ordering.API.Application.Command
{
    public class SetOrderStatusAwaitingStockValidationCommandHandler : IRequestHandler<SetOrderStatusAwaitingStockValidationCommand, bool>
    {
        private IOrderRepository _orderRepository;
        private ILogger<SetOrderStatusAwaitingStockValidationCommandHandler> _logger;

        public SetOrderStatusAwaitingStockValidationCommandHandler(IOrderRepository orderRepository, 
            ILogger<SetOrderStatusAwaitingStockValidationCommandHandler> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }

        public async Task<bool> Handle(SetOrderStatusAwaitingStockValidationCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(" [x] SetOrderStatusAwaitingValidationCommandHandler.Handle(): Handling SetOrderStatusAwaitingValidationCommand...");
            
            var orderToUpdate = await _orderRepository.GetAsync(request.OrderId);

            if(orderToUpdate == null)
            {
                _logger.LogError(" [x] SetOrderStatusAwaitingValidationCommandHandler.Handle(): Order is not found, command will not be executed.");
                return false;
            }

            orderToUpdate.SetStatusAwaitingStockValidation();
            

            return await _orderRepository.UnitOfWork.SaveEntitiesAsync();         
        }
    }
}