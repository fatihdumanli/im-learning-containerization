using System.Threading.Tasks;
using EventBus.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.API.Application.Command;
using Ordering.API.Application.IntegrationEvents.Events;

namespace Ordering.API.Application.IntegrationEvents.EventHandling
{
    public class OrderStockConfirmedIntegrationEventHandler : IIntegrationEventHandler<OrderStockConfirmedIntegrationEvent>
    {
        private ILogger<OrderStockConfirmedIntegrationEventHandler> _logger;
        private IMediator _mediator;

        public OrderStockConfirmedIntegrationEventHandler(IMediator mediator,
            ILogger<OrderStockConfirmedIntegrationEventHandler> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        public async Task Handle(OrderStockConfirmedIntegrationEvent @event)
        {
            _logger.LogInformation(" [x] OrderStockConfirmedIntegrationEventHandler:"  + 
                "Order stock confirmed, raising a command... (SetOrderStatusToStockConfirmedCommand)");

            var command = new SetOrderStatusToStockConfirmedCommand(orderId: @event.OrderId);

            await _mediator.Send(command);
        }
    }
}