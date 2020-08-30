using System.Threading.Tasks;
using Basket.API.IntegrationEvents.Events;
using EventBus.Abstractions;

namespace Basket.API.IntegrationEvents.EventHandling
{
    public class ProductPriceChangedIntegrationEventHandler : IIntegrationEventHandler<ProductPriceChangedIntegrationEvent>
    {
        public Task Handle(ProductPriceChangedIntegrationEvent @event)
        {
            //HANDLE logic
            return null;
        }
    }
}