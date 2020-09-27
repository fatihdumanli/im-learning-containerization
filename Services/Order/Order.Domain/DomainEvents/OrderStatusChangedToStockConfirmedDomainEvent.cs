using MediatR;

namespace Ordering.Domain.DomainEvents
{
    public class OrderStatusChangedToStockConfirmedDomainEvent : 
        INotification
    {
        public int OrderId { get; private set; }

        public OrderStatusChangedToStockConfirmedDomainEvent(int orderId)
        {
            this.OrderId = orderId;            
        }
    }
}