using MediatR;
using Ordering.Domain.SharedKernel;

namespace Ordering.Domain.DomainEvents
{
    public class OrderStatusChangedToStockConfirmedDomainEvent : 
        IDomainEvent
    {
        public int OrderId { get; private set; }

        public OrderStatusChangedToStockConfirmedDomainEvent(int orderId)
        {
            this.OrderId = orderId;            
        }
    }
}