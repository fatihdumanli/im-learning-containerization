using System;
using System.Diagnostics;
using System.Linq;
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
        private readonly ILogger<ProductPriceChangedIntegrationEventHandler> _logger;
        
        public ProductPriceChangedIntegrationEventHandler(IBasketRepository repo, ILogger<ProductPriceChangedIntegrationEventHandler> logger)
        {
            this._logger = logger;
            this._repository = repo;
        }
        public async Task Handle(ProductPriceChangedIntegrationEvent @event)
        {
            _logger.LogInformation(" [x] Message from inside of event handler!");
            _logger.LogInformation("\t [.] Event details");
            _logger.LogInformation("\t\t [.] Product id: {0}, Old price: {1}, New price: {2}", 
                @event.ProductId, @event.OldPrice, @event.NewPrice);

            //Get all user baskets.
            //Update all product prices.
            var users = _repository.GetUsers();

            foreach(var user in users)
            {
                var userBasket = await _repository.GetBasketAsync(user);
                var productsToUpdate = userBasket.Items.Where(b => b.ProductId == @event.ProductId).ToList();

                _logger.LogInformation(" [x] Checking basket for user: {0}", user);

                foreach(var product in productsToUpdate)
                {
                    _logger.LogInformation(" [x] Updating product in basket...");
                     var temp = product.UnitPrice;
                     product.UnitPrice = @event.NewPrice;
                     product.OldUnitPrice = temp;
                }
                
                await _repository.UpdateBasketAsync(userBasket);
            }
            
        }
    }
}