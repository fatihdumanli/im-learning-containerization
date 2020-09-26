using System.Collections.Generic;
using EventBus.Events;

namespace Catalog.API.IntegrationEvents
{
    public class OrderStatusChangedToAwaitingStockValidationIntegrationEvent : IntegrationEvent
    {
        public int OrderId { get; private set; }
        public IEnumerable<OrderStockItem> OrderStockItems { get; private set; }

        public OrderStatusChangedToAwaitingStockValidationIntegrationEvent(int orderId, IEnumerable<OrderStockItem> orderStockItems)
        {
            this.OrderId = orderId;
            this.OrderStockItems = orderStockItems;            
        }
    }


    public class OrderStockItem
    {
        public int ProductId { get; }
        public int Units { get; }

        public OrderStockItem(int productId, int units)
        {
            this.ProductId = productId;
            this.Units = units;
        }

    }
}