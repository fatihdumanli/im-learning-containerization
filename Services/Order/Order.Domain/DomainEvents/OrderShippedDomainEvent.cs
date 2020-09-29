using MediatR;
using Ordering.Domain.SharedKernel;

namespace Ordering.Domain.DomainEvents
{
    public class OrderShippedDomainEvent : IDomainEvent
    {
        public int OrderId { get; private set; }       

        public OrderShippedDomainEvent(int orderId)
        {
            this.OrderId = orderId;
        } 
    }
}