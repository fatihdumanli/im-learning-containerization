using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Basket.API.IntegrationEvents.Events;
using Basket.API.Model;
using EventBus.Abstractions;
using Microsoft.Extensions.Logging;

namespace Basket.API.IntegrationEvents.EventHandling
{
    public class ProductPriceChangedIntegrationEventHandler : IIntegrationEventHandler<ProductPriceChangedIntegrationEvent>
    {
        private readonly IBasketRepository _repository;
        
        public ProductPriceChangedIntegrationEventHandler(IBasketRepository repo)
        {
            this._repository = repo;
        }
        public Task Handle(ProductPriceChangedIntegrationEvent @event)
        {
            Debug.WriteLine("In handler method.");
            return null;
        }
    }
}