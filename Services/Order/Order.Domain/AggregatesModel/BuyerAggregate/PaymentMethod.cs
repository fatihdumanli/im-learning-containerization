using System;
using Ordering.Domain.Exceptions;
using Ordering.Domain.SharedKernel;

namespace Ordering.Domain.AggregatesModel.BuyerAggregate
{
    public class PaymentMethod : Entity
    {
        private string _alias;
        private string _cardNumber;
        private string _securityNumber;
        private string _cardHolderName;
        private DateTime _expiration;
        private int _cardTypeId;

        public CardType CardType { get; private set; }

        protected PaymentMethod() {}

        public PaymentMethod(int cardTypeId, string alias, string cardNumber, string securityNumber, string cardHolderName, DateTime expiration)
        {
            _cardNumber         =       string.IsNullOrWhiteSpace(cardNumber) ? throw new OrderDomainException("CardNumber can not be empty!") : cardNumber;
            _securityNumber     =       string.IsNullOrWhiteSpace(securityNumber) ? throw new OrderDomainException("CVV can not be empty!") : securityNumber;
            _cardHolderName     =       string.IsNullOrWhiteSpace(cardHolderName) ? throw new OrderDomainException("CardHolderName can not be empty!") : cardHolderName;

            if(expiration < DateTime.UtcNow)
            {
                throw new OrderDomainException("Expired card.");
            }

            _alias = alias;
            _expiration = expiration;
            _cardTypeId = cardTypeId;
        }



    }
}