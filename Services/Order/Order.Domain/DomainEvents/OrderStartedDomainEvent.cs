using MediatR;
using Ordering.Domain.AggregatesModel.OrderAggregate;
using Ordering.Domain.SharedKernel;
using System;

namespace Ordering.Domain.DomainEvents
{
    public class OrderStartedDomainEvent : IDomainEvent
    {
        public string Buyer { get; private set; }
        public string CardNumber { get; private set; }
        public string CardHolderName { get; private set; }
        public string Cvv { get; private set; }
        public DateTime Expiration { get; private set; }
        public int CardTypeId { get; private set; }
        public Order Order { get; private set; }        

        public OrderStartedDomainEvent(Order order, string buyer, string cardNumber, string cardHolderName,
            string cvv, DateTime expiration, int cardTypeId)
        {
            this.Order = order;
            this.Buyer = buyer;
            this.CardHolderName = cardHolderName;
            this.CardNumber = cardNumber;
            this.Cvv = cvv;
            this.Expiration = expiration;
            this.CardTypeId = cardTypeId;
        }

    }
}