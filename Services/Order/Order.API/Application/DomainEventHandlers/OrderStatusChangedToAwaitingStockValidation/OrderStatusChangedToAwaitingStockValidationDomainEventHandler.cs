using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.API.Application.IntegrationEvents.Events;
using Ordering.API.Application.IntegrationEvents.IntegrationEventService;
using Ordering.Domain.DomainEvents;
using Ordering.Domain.SharedKernel;

namespace Ordering.API.Application.DomainEventHandlers
{
    public class OrderStatusChangedToAwaitingStockValidationDomainEventHandler
        : IDomainEventHandler<OrderStatusChangedToAwaitingStockValidationDomainEvent>
    {
        private IOrderingIntegrationEventService _orderingIntegrationEventService;
        private ILogger<OrderStatusChangedToAwaitingStockValidationDomainEventHandler> _logger;
        public OrderStatusChangedToAwaitingStockValidationDomainEventHandler(IOrderingIntegrationEventService integrationEventService,
            ILogger<OrderStatusChangedToAwaitingStockValidationDomainEventHandler> logger)
        {
            _orderingIntegrationEventService = integrationEventService;     
            _logger = logger;       
        }
        public async Task Handle(OrderStatusChangedToAwaitingStockValidationDomainEvent notification, CancellationToken cancellationToken)
        {
            //Publish integration event here.

            //Prepare orderStockItems,
            //Publish integration event.

            var orderItems = notification.OrderItems;
            List<OrderStockItem> orderStockItems = new List<OrderStockItem>();
            
            foreach(var item in orderItems)
            {
                orderStockItems.Add(new OrderStockItem(item.ProductId, item.GetUnits()));
            }

            _logger.LogInformation(" [x] OrderStatusChangedToAwaitingStockValidationDomainEventHandler: Handling domain event and creating an OrderStatusChangedToAwaitingStockValidationIntegrationEvent to publish to RabbitMQ...");
            var integrationEvent = new OrderStatusChangedToAwaitingStockValidationIntegrationEvent(orderId: notification.OrderId,
                orderStockItems: orderStockItems);

            await _orderingIntegrationEventService.AddIntegrationEventToLog(@integrationEvent, null);
            await _orderingIntegrationEventService.PublishEvent(integrationEvent);

        }
    }
}