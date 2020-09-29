using MediatR;

namespace Ordering.Domain.DomainEvents
{
    public class OrderShippedDomainEvent : INotification
    {
        public int OrderId { get; private set; }       

        public OrderShippedDomainEvent(int orderId)
        {
            this.OrderId = orderId;
        } 
    }
}