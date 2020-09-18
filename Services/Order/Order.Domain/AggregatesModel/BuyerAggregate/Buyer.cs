using System.Collections.Generic;
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

        private ILogger<Buyer> _logger;
        protected Buyer() 
        {
            _paymentMethods = new List<PaymentMethod>();
        }


        public Buyer(string identity, string name, ILogger<Buyer> logger = null) : this()
        {
            _logger = logger;
            IdentityGuid = string.IsNullOrWhiteSpace(identity) ? throw new OrderDomainException("Buyer identity can not be empty!") : identity;
            Name         = string.IsNullOrWhiteSpace(name) ? throw new OrderDomainException("Buyer name can not be empty!") : name;
            _logger.LogInformation(" [x] Buyer: Created a buyer Aggregate.");
        }

        public void ValidatePaymentMethod()
        {
            
            
        }


    }
}