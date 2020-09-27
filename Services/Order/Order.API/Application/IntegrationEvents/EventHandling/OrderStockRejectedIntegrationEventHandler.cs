using System.Threading.Tasks;
using EventBus.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.API.Application.Command;
using Ordering.API.Application.IntegrationEvents.Events;

namespace Ordering.API.Application.IntegrationEvents.EventHandling
{
    public class OrderStockRejectedIntegrationEventHandler : IIntegrationEventHandler<OrderStockRejectedIntegrationEvent>
    {
        private ILogger<OrderStockRejectedIntegrationEventHandler> _logger;
        private IMediator _mediator;
        
        public OrderStockRejectedIntegrationEventHandler(
            IMediator mediator,
            ILogger<OrderStockRejectedIntegrationEventHandler> logger)
        {
            _logger = logger;     
            _mediator = mediator;       
        }

        public async Task Handle(OrderStockRejectedIntegrationEvent @event)
        {
            _logger.LogInformation(" [x] OrderStockRejectedIntegrationEvent: Order stock rejected, raising a command (SetOrderStatusToStockRejectedCommand)" +
                " for order with id {0}", @event.OrderId);

            var command = new SetOrderStatusToStockRejectedCommand(orderId: @event.OrderId);

            await _mediator.Send(command);       
        }
    }
}