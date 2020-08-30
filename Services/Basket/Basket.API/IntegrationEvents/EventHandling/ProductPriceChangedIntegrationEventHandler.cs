using System.Diagnostics;
using System.Threading.Tasks;
using Basket.API.IntegrationEvents.Events;
using EventBus.Abstractions;
using Microsoft.Extensions.Logging;

namespace Basket.API.IntegrationEvents.EventHandling
{
    public class ProductPriceChangedIntegrationEventHandler : IIntegrationEventHandler<ProductPriceChangedIntegrationEvent>
    {
    
        public Task Handle(ProductPriceChangedIntegrationEvent @event)
        {
            Debug.WriteLine("Handler methoduna girdim.");
            return null;
        }
    }
}