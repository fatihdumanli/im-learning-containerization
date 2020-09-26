
using System.Threading.Tasks;
using Catalog.API.Model;
using EventBus.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Catalog.API.IntegrationEvents
{
    public class OrderStatusChangedToAwaitingStockValidationIntegrationEventHandler : IIntegrationEventHandler<OrderStatusChangedToAwaitingStockValidationIntegrationEvent>
    {
        private CatalogContext _context;
        private ILogger<OrderStatusChangedToAwaitingStockValidationIntegrationEventHandler> _logger;
        private IEventBus _eventBus;

        public OrderStatusChangedToAwaitingStockValidationIntegrationEventHandler(
            CatalogContext context,
            IEventBus eventBus,
            ILogger<OrderStatusChangedToAwaitingStockValidationIntegrationEventHandler> logger)
        {
            _eventBus = eventBus;
            _context = context;
            _logger = logger;
        }
        
        public async Task Handle(OrderStatusChangedToAwaitingStockValidationIntegrationEvent @event)
        {
            foreach(var item in @event.OrderStockItems)
            {
                var catalogItem = await _context.CatalogItems.SingleOrDefaultAsync(c => c.Id == item.ProductId);
                
                if(catalogItem.AvailableStock < item.Units)
                {
                    var rejectedIntegrationEvent = new OrderStockRejectedIntegrationEvent(@event.OrderId);
                    _eventBus.Publish(rejectedIntegrationEvent);
                    _logger.LogInformation(" [x] Catalog Service: OrderStatusChangedToAwaitingStockValidationIntegrationEventHandler: Stock is rejected for product id {0}", item.ProductId);
                    return;
                }                
            }


            var confirmedIntegrationEvent = new OrderStockConfirmedIntegrationEvent(@event.OrderId);
            _logger.LogInformation(" [x] Catalog Service: OrderStatusChangedToAwaitingStockValidationIntegrationEventHandler: Stock is confirmed for all products successfully!");
            _eventBus.Publish(confirmedIntegrationEvent);
        }
    }
}