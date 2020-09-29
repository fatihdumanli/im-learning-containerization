using Ordering.Domain.AggregatesModel.BuyerAggregate;
using Ordering.Domain.SharedKernel;

namespace Ordering.Domain.DomainEvents
{
    public class PaymentMethodValidatedDomainEvent : IDomainEvent
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