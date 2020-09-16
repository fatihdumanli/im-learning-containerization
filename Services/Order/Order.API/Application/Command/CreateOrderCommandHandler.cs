using CommandDistpaching;
using Microsoft.Extensions.Logging;
using Ordering.Domain.AggregatesModel.OrderAggregate;

namespace Ordering.API.Application.Command
{
    public class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand>
    {
        private ILogger<CreateOrderCommandHandler> _logger;

        public CreateOrderCommandHandler(ILogger<CreateOrderCommandHandler> logger)
        {
            _logger = logger;
        }

        public void Handle(CreateOrderCommand command)
        {
            
        }
    }
}