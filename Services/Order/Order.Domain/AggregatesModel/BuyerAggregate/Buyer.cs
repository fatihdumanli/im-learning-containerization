using System.Collections.Generic;
using Order.Domain.Exceptions;
using Order.Domain.SharedKernel;

namespace Order.Domain.AggregatesModel.BuyerAggregate
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
        }

    }
}