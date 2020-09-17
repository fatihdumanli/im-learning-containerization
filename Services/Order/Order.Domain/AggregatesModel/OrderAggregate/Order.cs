using System;
using System.Collections.Generic;
using System.Linq;
using Ordering.Domain.DomainEvents;
using Ordering.Domain.SharedKernel;

namespace Ordering.Domain.AggregatesModel.OrderAggregate
{
    public class Order : Entity, IAggregateRoot
    {
        private int? _buyerId;
        public int? GetBuyerId => _buyerId;
        private int? _paymentMethodId;
        public OrderStatus OrderStatus { get; private set; }
        private int _orderStatusId;

        public Address Address { get; private set; }

        private DateTime _orderDate;

        private readonly List<OrderItem> _orderItems;
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;
        
        public Order(string userId, string userName, Address address, int cardTypeId, string cardNumber, string cardSecurityNumber,
                string cardHolderName, DateTime cardExpiration, int? buyerId = null, int? paymentMethodId = null) : this()
        {
                _buyerId = buyerId;
                _paymentMethodId = paymentMethodId;
                //Order ilk kaydedilirken submitted statusunde kaydediliyor. (OrderStatusId: 1)
                _orderStatusId = OrderStatus.Submitted.Id;
                _orderDate = DateTime.UtcNow;
                Address = address;              

                
                this.AddDomainEvent(new OrderStartedDomainEvent(userId));
                
        }
        protected Order()
        {  
            _orderItems = new List<OrderItem>();            
        }

        public void AddOrderItem(int productId, string productName, decimal unitPrice, decimal discount, string pictureUrl, int units = 1)
        {
            var existingOrderForProduct = _orderItems.Where(o => o.ProductId == productId).SingleOrDefault();

            if(existingOrderForProduct != null)
            {
                existingOrderForProduct.AddUnits(units);
            }

            else
            {
                var orderItem = new OrderItem(productId, productName, pictureUrl, unitPrice, discount, units);
                this._orderItems.Add(orderItem);
            }
        }
        
    }
}