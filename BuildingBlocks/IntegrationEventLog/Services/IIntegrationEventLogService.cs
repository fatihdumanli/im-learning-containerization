using System;
using System.Threading.Tasks;
using EventBus.Events;
using Microsoft.EntityFrameworkCore.Storage;

namespace IntegrationEventLog
{
    public interface IIntegrationEventLogService
    {
        Task SaveEventAsync(IntegrationEvent @event, IDbContextTransaction transaction);
        Task MarkEventAsPublishedAsync(Guid eventId);
        Task MarkEventAsInProgressAsync(Guid eventId);
        Task MarkEventAsFailedAsync(Guid eventId);
    }
}