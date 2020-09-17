using DomainDispatching.DomainEvent;
using System;

namespace Ordering.Domain.DomainEvents
{
    public class OrderStartedDomainEvent : IDomainEvent
    {
        public string Buyer { get; set; }

        public OrderStartedDomainEvent(string buyer)
        {
            this.Buyer = buyer;
        }

    }
}