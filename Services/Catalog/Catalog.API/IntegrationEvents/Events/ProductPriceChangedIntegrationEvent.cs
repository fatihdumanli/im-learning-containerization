using System;
using EventBus.Events;
using Newtonsoft.Json;

namespace Catalog.API.IntegrationEvents
{
    public class ProductPriceChangedIntegrationEvent : IntegrationEvent
    {
        [JsonProperty("ProductId")]
        public int ProductId { get; set; }
        [JsonProperty("OldPrice")]
        public decimal OldPrice { get; set; }
        [JsonProperty("NewPrice")]
        public decimal NewPrice { get; set; }
        
        [JsonConstructor]
        public ProductPriceChangedIntegrationEvent(int productId, decimal oldPrice, decimal newPrice)
        {
            this.ProductId = productId;
            this.OldPrice = oldPrice;
            this.NewPrice = newPrice;            
        }
    }

}