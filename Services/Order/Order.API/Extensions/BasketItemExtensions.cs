using System.Collections.Generic;
using Order.API.Application.Models;
using static Order.API.Application.Command.CreateOrderCommand;

namespace Order.API.Extensions
{
    public static class BasketItemExtensions
    {

        public static IEnumerable<OrderItemDto> ToOrderItemsDto(this IEnumerable<BasketItem> basketItems)
        {
            foreach(var item in basketItems)
            {
                yield return item.ToOrderItemDto();
            }
        }

        public static OrderItemDto ToOrderItemDto(this BasketItem item)
        {
            return new OrderItemDto() { ProductId = item.ProductId, ProductName = item.ProductName, UnitPrice = item.UnitPrice,
              Units = item.Quantity };
        }
    }
}