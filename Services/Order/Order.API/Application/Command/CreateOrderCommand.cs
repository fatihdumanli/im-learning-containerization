using System;
using System.Collections.Generic;
using System.Linq;
using Ordering.API.Application.Models;

namespace Ordering.API.Application.Command
{

    public class OrderItemDto
    {
        public int ProductId { get; private set; }
        public string ProductName { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal Units { get; private set; }

        public OrderItemDto(int productId, string productName, decimal unitPrice, decimal units)
        {
            this.ProductId = ProductId;
            this.ProductName = productName;
            this.UnitPrice = unitPrice;
            this.Units = units;            
        }
    }

    public static class BasketItemExtensions
    {
        public static IEnumerable<OrderItemDto> ToOrderItemDtoList(this IEnumerable<BasketItem> basketItems)
        {
            foreach(var item in basketItems)
            {
                yield return item.ToOrderItemDTO();
            }
        }


        public static OrderItemDto ToOrderItemDTO(this BasketItem basketItem)
        {
            return new OrderItemDto(basketItem.ProductId, basketItem.ProductName, basketItem.UnitPrice, basketItem.Quantity);
        }
    }


    public class CreateOrderCommand : DomainDispatching.Commanding.Command
    {

        public CreateOrderCommand(string buyerId, string street, string city, string state, string zipCode, 
            string country, string cardNumber, string cardHolderName, string cvv, DateTime expiration, int cardTypeId,
             List<BasketItem> basketItems)
        {
            this.BuyerId = buyerId;
            this.City = city;
            this.Country = country;
            this.State = state;
            this.Street = street;
            this.ZipCode = zipCode;
            this.CardNumber = cardNumber;
            this.CardTypeId = cardTypeId;
            this.CardHolderName = cardHolderName;
            this.CardExpiration = expiration;
            this.Cvv = cvv;
            this.OrderItems = basketItems.ToOrderItemDtoList().ToList();
        }


        public List<OrderItemDto> OrderItems { get; private set; }
        public string Street { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Country { get; private set; }
        public string ZipCode { get; private set; }
        public string BuyerId { get; private set; }
        public int CardTypeId { get; private set; }
        public string CardNumber { get; private set; }
        public string CardHolderName { get; private set; }
        public string Cvv { get; private set; }
        public DateTime CardExpiration { get; private set; }

    }
}