using System.Threading.Tasks;
using EventBus.Events;

namespace EventBus.Abstractions
{
    public interface IIntegrationEventHandler<TIntegrationEvent> 
        where TIntegrationEvent : IntegrationEvent
    {
        Task Handle(TIntegrationEvent @event);
    }
}