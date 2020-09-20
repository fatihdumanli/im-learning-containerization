using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using EventBus.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace IntegrationEventLog
{
    public class IntegrationEventLogService : IIntegrationEventLogService
    {
        private readonly IntegrationEventLogContext _integrationEventLogContext;
        private readonly DbConnection _dbConnection;
        private ILogger<IntegrationEventLogService> _logger;
        public IntegrationEventLogService(DbConnection dbConnection, ILogger<IntegrationEventLogService> logger)
        {
            _dbConnection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
            
            _logger = logger;
            _integrationEventLogContext = new IntegrationEventLogContext(
                new DbContextOptionsBuilder<IntegrationEventLogContext>()
                    .UseSqlServer(_dbConnection)
                    .Options);
            _logger.LogInformation(" [x] IntegrationEventLogService: Creating a new instance...");
        }

        public Task MarkEventAsFailedAsync(Guid eventId)
        {   
            _logger.LogInformation(" [x] IntegrationEventLogService.MarkEventAsFailed(): Marking event {0} as failed...", eventId);
            return this.UpdateEventStatus(eventId, EventStateEnum.PublishedFailed);
        }

        public Task MarkEventAsInProgressAsync(Guid eventId)
        {
            _logger.LogInformation(" [x] IntegrationEventLogService.MarkEventAsInProgress(): Marking event {0} as in progress...", eventId);
            return this.UpdateEventStatus(eventId, EventStateEnum.InProgress);
        }

        public Task MarkEventAsPublishedAsync(Guid eventId)
        {
            _logger.LogInformation(" [x] IntegrationEventLogService.MarkEventAsPublished(): Marking event {0} as published...", eventId);
            return this.UpdateEventStatus(eventId, EventStateEnum.Published);
        }

        //Değişikliğin yapıldığı asıl connection la aynı transaction ı kullanıyor.
        public Task SaveEventAsync(IntegrationEvent @event, IDbContextTransaction transaction)
        {
            _logger.LogInformation(" [x] IntegrationEventLogService.SaveEventAsync(): Saving event {0} with transaction {1}...", @event.Id,
                transaction.TransactionId);
                
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));

            var eventLogEntry = new IntegrationEventLogEntry(@event, transaction.TransactionId);

            _integrationEventLogContext.Database.UseTransaction(transaction.GetDbTransaction());
            _integrationEventLogContext.IntegrationEventLogs.Add(eventLogEntry);

            return _integrationEventLogContext.SaveChangesAsync();        
        }

        private Task UpdateEventStatus(Guid eventId, EventStateEnum status)
        {
            _logger.LogInformation(" [x] IntegrationEventLogService.UpdateEventStatus(): Marking event {0} status...", eventId);
            var eventLogEntry = _integrationEventLogContext.IntegrationEventLogs.Single(ie => ie.EventId == eventId);
            eventLogEntry.State = status;

            _integrationEventLogContext.IntegrationEventLogs.Update(eventLogEntry);

            return _integrationEventLogContext.SaveChangesAsync();
        }
    }
}