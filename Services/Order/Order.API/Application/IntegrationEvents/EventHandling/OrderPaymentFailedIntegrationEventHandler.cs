using System.Threading.Tasks;
using EventBus.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.API.Application.Command;
using Ordering.API.Application.IntegrationEvents.Events;

namespace Ordering.API.Application.IntegrationEvents.EventHandling
{
    public class OrderPaymentFailedIntegrationEventHandler
        : IIntegrationEventHandler<OrderPaymentFailedIntegrationEvent>
    {
        private ILogger<OrderPaymentFailedIntegrationEventHandler> _logger;
        private IMediator _mediator;
        
        public OrderPaymentFailedIntegrationEventHandler(
            IMediator mediator,
            ILogger<OrderPaymentFailedIntegrationEventHandler> logger)
        {
            this._logger = logger;
            this._mediator = mediator;
        }
        
        public async Task Handle(OrderPaymentFailedIntegrationEvent @event)
        {

            _logger.LogInformation(" [x] OrderPaymentFailedIntegrationEvent: Creating new CancelOrderCommand.");
            
            var command = new CancelOrderCommand(orderId: @event.OrderId, reason: "Payment failed");
            await _mediator.Send(command);
        }
    }
}