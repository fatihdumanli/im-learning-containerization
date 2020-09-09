using System;
using System.Collections.Generic;
using System.Linq;
using MediatR;
using Order.API.Application.Models;
using Order.API.Extensions;

namespace Order.API.Application.Command
{
    public class CreateOrderCommand : IRequest<bool>
    {
    
        public class OrderItemDto
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public decimal UnitPrice { get; set; }
            public decimal Discount { get; set; }
            public int Units { get; set; }
            public string PictureUrl { get; set; }

        }

        private readonly List<OrderItemDto> _orderItems;
        public string UserId { get; private set; }

        public string UserName { get; private set; }

        public int OrderNumber { get; private set;}

        public string City { get; private set; }

        public string Street { get; private set; }

        public string State { get; private set; }

        public string Country { get; private set; }

        public string ZipCode { get; private set; }

        public string CardNumber { get; private set; }

        public string CardHolderName { get; private set; }

        public DateTime CardExpiration { get; private set; }

        public string CardSecurityNumber { get; private set; }

        public int CardTypeId { get; private set; }

        public IEnumerable<OrderItemDto> OrderItems => _orderItems;

        public CreateOrderCommand() {
            _orderItems = new List<OrderItemDto>();
        }    

        public CreateOrderCommand(List<BasketItem> basketItems, string userId, string userName, string city, string street,
            string state, string country, string zipCode, string cardNumber, string cardHolderName, DateTime cardExpiration, 
            string cardSecurityNumber, int cardTypeId) : this()
        {
            this._orderItems = basketItems.ToOrderItemsDto().ToList();
            this.UserId = userId;
            this.UserName = userName;
            this.City = city;
            this.Street = street;
            this.State = state;
            this.Country = country;
            this.ZipCode = zipCode;
            this.CardNumber = cardNumber;
            this.CardHolderName = cardHolderName;
            this.CardExpiration = cardExpiration;
            this.CardSecurityNumber = cardSecurityNumber;
            this.CardTypeId = cardTypeId;
        }

    }
}