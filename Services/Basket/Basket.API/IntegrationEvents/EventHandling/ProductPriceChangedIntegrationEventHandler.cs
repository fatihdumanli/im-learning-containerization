using System.Linq;
using System.Threading.Tasks;
using Basket.API.IntegrationEvents.Events;
using Basket.API.Model;
using EventBus.Abstractions;

namespace Basket.API.IntegrationEvents.EventHandling
{
    public class ProductPriceChangedIntegrationEventHandler :
        IIntegrationEventHandler<ProductPriceChangedIntegrationEvent>
    {

        private readonly IBasketRepository _repository;
        
        public ProductPriceChangedIntegrationEventHandler(IBasketRepository repo)
        {
            this._repository = repo;
        }

        public async Task Handle(ProductPriceChangedIntegrationEvent @event)
        {
            /*
                ProductPriceChangeEvent ile gelecek data,
                    -ProductId
                    -OldPrice
                    -NewPrice 

                olacak.

                Tüm basketlerdeki bu ProductId ile ilgili kaydın fiyatını güncellemek lazım.
            */

            var customerBaskets = _repository.GetUsers();

            foreach(var customerId in customerBaskets)
            {
                var basket = await _repository.GetBasketAsync(customerId); 
                await this.UpdatePriceInBasket(@event, basket: basket);
            }

        }

        //Her bir customer için çağrılıyor.
        private async Task UpdatePriceInBasket(ProductPriceChangedIntegrationEvent @event, CustomerBasket basket)
        {
            var productId = @event.ProductId;

            var itemsToUpdate = basket.Items.Where(b => b.ProductId == productId).ToList();

            if(itemsToUpdate == null)
                return;

            foreach(var basketItem in itemsToUpdate)
            {
                var oldUnitPrice = basketItem.UnitPrice;
                basketItem.UnitPrice = @event.NewPrice;
                basketItem.OldUnitPrice = oldUnitPrice;
            }

            await _repository.UpdateBasketAsync(basket);
        }


    }
}