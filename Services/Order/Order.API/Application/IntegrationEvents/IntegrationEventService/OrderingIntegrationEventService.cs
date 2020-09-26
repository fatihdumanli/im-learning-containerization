using System;
using System.Linq;
using System.Threading.Tasks;
using EventBus.Abstractions;
using EventBus.Events;
using IntegrationEventLog;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Ordering.API.Application.IntegrationEvents.IntegrationEventService
{
    public class OrderingIntegrationEventService : IOrderingIntegrationEventService
    {
        private IEventBus _eventBus;
        private ILogger<OrderingIntegrationEventService> _logger;
        
        private IntegrationEventLogContext _context;

        public OrderingIntegrationEventService(IntegrationEventLogContext context,
             IEventBus eventBus,
             ILogger<OrderingIntegrationEventService> logger)
        {
            this._context = context;
            this._eventBus = eventBus;
            this._logger = logger;
            _logger.LogInformation(" [x] Creating an instance of OrderingIntegrationEventService.");
        }

    
        public async Task AddIntegrationEventToLog(IntegrationEvent @event, IDbContextTransaction transaction)
        {
           
            var integrationEventLogEntry = new IntegrationEventLogEntry(@event, Guid.NewGuid());
            _context.IntegrationEventLogs.Add(integrationEventLogEntry);

              _logger.LogInformation(" [x] OrderingIntegrationService.AddIntegrationEventToLog(): Added integration event to integrationEventLog: {0}",
                JsonConvert.SerializeObject(@event));
                
            await _context.SaveChangesAsync();
        }

        public async Task PublishEvent(IntegrationEvent @event)
        {
            _eventBus.Publish(@event);           
        }
    }
}