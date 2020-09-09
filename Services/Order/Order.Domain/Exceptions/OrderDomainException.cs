using System;

namespace Ordering.Domain.Exceptions
{
    public class OrderDomainException : Exception
    {
        public OrderDomainException() {}

        public OrderDomainException(string message) : base(message) {}

        public OrderDomainException(string message, Exception innerException) : base(message, innerException) {}
    }
}