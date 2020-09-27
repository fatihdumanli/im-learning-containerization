using System.Threading.Tasks;
using EventBus.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.API.Application.Command;
using Ordering.API.Application.IntegrationEvents.Events;

namespace Ordering.API.Application.IntegrationEvents.EventHandling
{
    public class OrderPaymentSucceededIntegrationEventHandler :
        IIntegrationEventHandler<OrderPaymentSucceededIntegrationEvent>
    {
        private ILogger<OrderPaymentSucceededIntegrationEventHandler> _logger;
        private IMediator _mediator;
        
        public OrderPaymentSucceededIntegrationEventHandler(
            IMediator mediator,
            ILogger<OrderPaymentSucceededIntegrationEventHandler> logger)
        {
            _mediator = mediator;
            _logger = logger;            
        }

        public async Task Handle(OrderPaymentSucceededIntegrationEvent @event)
        {
            _logger.LogInformation(" [x] OrderPaymentSucceededIntegrationEventHandler: Payment was succeeded. Creating command SetOrderStatusToPaidCommand" + 
                "to transition order status STOCKCONFIRMED -------> PAID");

            var command = new SetOrderStatusToPaidCommand(orderId: @event.OrderId);
            await _mediator.Send(command);
        }
    }
}