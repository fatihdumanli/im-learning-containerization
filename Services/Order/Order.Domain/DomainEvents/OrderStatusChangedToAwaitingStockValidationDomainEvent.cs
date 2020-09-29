using System.Collections.Generic;
using MediatR;
using Ordering.Domain.AggregatesModel.OrderAggregate;
using Ordering.Domain.SharedKernel;

namespace Ordering.Domain.DomainEvents
{
    public class OrderStatusChangedToAwaitingStockValidationDomainEvent : IDomainEvent
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