using EventBus.Events;

namespace Ordering.API.Application.IntegrationEvents.IntegrationEventService
{
    public interface IOrderIntegrationEventService
    {
        void AddIntegrationEventToLog(IntegrationEvent @event);
        void PublishEvents();
    }
}