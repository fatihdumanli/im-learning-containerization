using DomainDispatching.DomainEvent;
using MediatR;
using System;

namespace Ordering.Domain.DomainEvents
{
    public class OrderStartedDomainEvent : INotification
    {
        public string Buyer { get; private set; }
        public string CardNumber { get; private set; }
        public string CardHolderName { get; private set; }
        public string Cvv { get; private set; }
        public DateTime Expiration { get; private set; }
        public int CardTypeId { get; private set; }

        public OrderStartedDomainEvent(string buyer, string cardNumber, string cardHolderName,
            string cvv, DateTime expiration, int cardTypeId)
        {
            this.Buyer = buyer;
            this.CardHolderName = cardHolderName;
            this.CardNumber = cardNumber;
            this.Cvv = cvv;
            this.Expiration = expiration;
            this.CardTypeId = cardTypeId;
        }

    }
}