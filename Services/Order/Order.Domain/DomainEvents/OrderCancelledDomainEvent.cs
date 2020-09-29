using MediatR;

namespace Ordering.Domain.DomainEvents
{
    public class OrderCancelledDomainEvent : INotification
    {
        public int OrderId { get; private set; }

        public string CancellationReason { get; private set; }

        public OrderCancelledDomainEvent(int orderId, string reason)
        {
            this.OrderId = orderId;
            this.CancellationReason = reason;            
        }
    }
}