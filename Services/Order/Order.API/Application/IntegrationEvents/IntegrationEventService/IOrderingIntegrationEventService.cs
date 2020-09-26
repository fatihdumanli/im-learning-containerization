using System.Threading.Tasks;
using EventBus.Events;
using Microsoft.EntityFrameworkCore.Storage;

namespace Ordering.API.Application.IntegrationEvents.IntegrationEventService
{
    public interface IOrderingIntegrationEventService
    {
        Task AddIntegrationEventToLog(IntegrationEvent @event, IDbContextTransaction transaction);
        Task PublishEvent(IntegrationEvent @event);
    }
}