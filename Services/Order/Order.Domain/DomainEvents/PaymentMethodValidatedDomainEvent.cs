using MediatR;
using Ordering.Domain.AggregatesModel.BuyerAggregate;

namespace Ordering.Domain.DomainEvents
{
    public class PaymentMethodValidatedDomainEvent : INotification
    {
        public int OrderId { get; private set; }
        public Buyer Buyer { get; private set; }

        public PaymentMethod PaymentMethod { get; private set; }

        public PaymentMethodValidatedDomainEvent(int orderId, Buyer buyer, PaymentMethod paymentMethod)
        {
            this.OrderId = orderId;
            this.Buyer = buyer;
            this.PaymentMethod = paymentMethod;            
        }             
        
    }
}