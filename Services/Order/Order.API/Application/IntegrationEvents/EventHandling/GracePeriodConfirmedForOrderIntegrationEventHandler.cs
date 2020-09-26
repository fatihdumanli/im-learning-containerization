using System.Threading.Tasks;
using EventBus.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.API.Application.Command;
using Ordering.API.Application.IntegrationEvents.Events;

namespace Ordering.API.Application.IntegrationEvents.EventHandling
{
    public class GracePeriodConfirmedForOrderIntegrationEventHandler : IIntegrationEventHandler<GracePeriodConfirmedForOrderIntegrationEvent>
    {
        private ILogger<GracePeriodConfirmedForOrderIntegrationEventHandler> _logger;
        private IMediator _mediator;

        public GracePeriodConfirmedForOrderIntegrationEventHandler(IMediator mediator,
            ILogger<GracePeriodConfirmedForOrderIntegrationEventHandler> logger)
        {
            this._logger = logger;
            this._mediator = mediator;
        }
        public async Task Handle(GracePeriodConfirmedForOrderIntegrationEvent @event)
        {
            _logger.LogInformation(" [x] GracePeriodConfirmedForOrderIntegrationEventHandler.Handle(): Received an GracePeriodConfirmedForOrderIntegrationEvent for Order with id {0}...", @event.OrderId);
            
            var command = new SetOrderStatusAwaitingStockValidationCommand(orderId: @event.OrderId);
            _logger.LogInformation(" [x] GracePeriodConfirmedForOrderIntegrationEventHandler.Handle(): Created new SetOrderStatusAwaitingValidationCommand.");
            
            await _mediator.Send(command);
        }
    }
}
