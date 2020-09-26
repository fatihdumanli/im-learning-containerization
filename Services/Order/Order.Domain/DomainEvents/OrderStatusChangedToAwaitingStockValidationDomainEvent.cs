using System.Collections.Generic;
using MediatR;
using Ordering.Domain.AggregatesModel.OrderAggregate;

namespace Ordering.Domain.DomainEvents
{
    public class OrderStatusChangedToAwaitingStockValidationDomainEvent : INotification
    {
        public int OrderId { get; private set; }
        public IEnumerable<OrderItem> OrderItems { get; private set; }
        
        public OrderStatusChangedToAwaitingStockValidationDomainEvent(int orderId, IEnumerable<OrderItem> orderItems)
        {
            this.OrderId = orderId;
            this.OrderItems = orderItems;
        }

    }
}