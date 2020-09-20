using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Ordering.Domain.Exceptions;
using Ordering.Domain.SharedKernel;

namespace Ordering.Domain.AggregatesModel.BuyerAggregate
{
    public class Buyer : Entity, IAggregateRoot
    {
        public string IdentityGuid { get; private set; }
        public string Name { get; private set; }

        private List<PaymentMethod> _paymentMethods;

        public IEnumerable<PaymentMethod> PaymentMethods => _paymentMethods.AsReadOnly();

        protected Buyer() 
        {
            _paymentMethods = new List<PaymentMethod>();
        }


        public Buyer(string identity, string name) : this()
        {
            IdentityGuid = string.IsNullOrWhiteSpace(identity) ? throw new OrderDomainException("Buyer identity can not be empty!") : identity;
            Name         = string.IsNullOrWhiteSpace(name) ? throw new OrderDomainException("Buyer name can not be empty!") : name;
            //_logger.LogInformation(" [x] Buyer: Created a buyer Aggregate.");
        }

        public void ValidatePaymentMethod(string cardNumber, string cardHolderName, string cvv, int cardTypeId, DateTime expiration)
        {
            throw new Exception("Payment validation failed.");
            //_logger.LogInformation(" Buyer.ValidatePaymentMethod(): Payment method is being validated for the buyer: {0}", this.Name);
            var paymentMethod = _paymentMethods.SingleOrDefault(p => p.IsEqualsTo(cardTypeId, cardNumber, expiration));
            var isPaymentMethodExisting = paymentMethod != null;
            
            if(isPaymentMethodExisting)
            {
                //addDomainEvent validated
                //_logger.LogInformation(" Buyer.ValidatePaymentMethod(): Payment method is already existed for buyer: {0}", Name);
                return;
            }

                //_logger.LogInformation(" Buyer.ValidatePaymentMethod(): Payment method has not been used before, creating new instance of PaymentMethod.",
                //Name);

             paymentMethod = new PaymentMethod(cardTypeId: cardTypeId, alias:$"Payment Method on {DateTime.UtcNow}", cardNumber: cardNumber,
                    securityNumber: cvv, cardHolderName: cardHolderName, expiration: expiration);

            _paymentMethods.Add(paymentMethod);

            //addDomainEvent validated
            return;
        }


    }
}