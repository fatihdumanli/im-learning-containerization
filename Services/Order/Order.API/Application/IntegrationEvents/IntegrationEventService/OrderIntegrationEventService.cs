using EventBus.Abstractions;
using EventBus.Events;
using IntegrationEventLog;
using Microsoft.Extensions.Logging;

namespace Ordering.API.Application.IntegrationEvents.IntegrationEventService
{
    public class OrderIntegrationEventService : IOrderIntegrationEventService
    {
        private IntegrationEventLogContext _context;
        private IEventBus _eventBus;
        private ILogger<OrderIntegrationEventService> _logger;

        public OrderIntegrationEventService(IntegrationEventLogContext context,
             IEventBus eventBus,
             ILogger<OrderIntegrationEventService> logger)
        {
            this._context = context;
            this._eventBus = eventBus;
            this._logger = logger;
            _logger.LogInformation(" [x] Creating an instance of OrderIntegrationEventService.");
        }

        
        public void AddIntegrationEventToLog(IntegrationEvent @event)
        {
            throw new System.NotImplementedException();
        }

        public void PublishEvents()
        {
            throw new System.NotImplementedException();
        }
    }
}