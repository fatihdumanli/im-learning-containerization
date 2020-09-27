using System.Threading.Tasks;
using EventBus.Abstractions;
using Microsoft.Extensions.Logging;

namespace Payment.IntegrationEvents
{
    public class OrderStatusChangedToStockConfirmedIntegrationEventHandler :
        IIntegrationEventHandler<OrderStatusChangedToStockConfirmedIntegrationEvent>
    {
        private ILogger<OrderStatusChangedToStockConfirmedIntegrationEventHandler> _logger;
        
        public OrderStatusChangedToStockConfirmedIntegrationEventHandler(ILogger<OrderStatusChangedToStockConfirmedIntegrationEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(OrderStatusChangedToStockConfirmedIntegrationEvent @event)
        {
            _logger.LogInformation(" [x] OrderStatusChangedToStockConfirmedIntegrationEventHandler: Handling integration event..."); 
            return null;
        }
    }
}