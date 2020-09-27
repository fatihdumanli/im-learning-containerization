using System;
using EventBus.Events;
using Ordering.Domain.AggregatesModel.BuyerAggregate;
using Ordering.Domain.AggregatesModel.OrderAggregate;

namespace Ordering.API.Application.IntegrationEvents.Events
{
    public class OrderStatusChangedToStockConfirmedIntegrationEvent: IntegrationEvent
    {
        public int OrderId { get; private set; }
        public string Buyer { get; private set; }
        public PaymentMethodDTO PaymentMethod { get; private set; } 

        public OrderStatusChangedToStockConfirmedIntegrationEvent(int orderId, string buyer, PaymentMethodDTO paymentMethod)
        {
            this.OrderId = orderId;
            this.Buyer = buyer;
            this.PaymentMethod = paymentMethod;
        }

    }

  
    public class PaymentMethodDTO
    {

        public static PaymentMethodDTO ToPaymentMethodDto(PaymentMethod pm)
        {
            return new PaymentMethodDTO(pm.CardHolderName, pm.CardNumber, pm.CVV, pm.Expiration);
        }

        public PaymentMethodDTO(string cardHolderName,
            string cardNumber, string cvv, DateTime expiration)
        {
            this.CardExpiration = expiration;
            this.CardNumber = cardNumber;
            this.CardHolderName = cardHolderName;
            this.CVV = cvv;
        }

        public string CardHolderName { get; private set; }
        public string CardNumber { get; private set; }
        public DateTime CardExpiration { get; private set; }
        public string CVV { get; private set; }        
    }
}