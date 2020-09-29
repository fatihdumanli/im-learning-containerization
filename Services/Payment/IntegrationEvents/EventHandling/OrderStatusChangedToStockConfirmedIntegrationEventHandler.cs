using System.Threading;
using System.Threading.Tasks;
using EventBus.Abstractions;
using Microsoft.Extensions.Logging;

namespace Payment.IntegrationEvents
{
    public class OrderStatusChangedToStockConfirmedIntegrationEventHandler :
        IIntegrationEventHandler<OrderStatusChangedToStockConfirmedIntegrationEvent>
    {
        private ILogger<OrderStatusChangedToStockConfirmedIntegrationEventHandler> _logger;
        private IEventBus _eventBus;

        bool isPaymentSuccessful = false;
        
        public OrderStatusChangedToStockConfirmedIntegrationEventHandler(
            IEventBus eventBus,
            ILogger<OrderStatusChangedToStockConfirmedIntegrationEventHandler> logger)
        {
            _eventBus = eventBus;
            _logger = logger;
        }

        public Task Handle(OrderStatusChangedToStockConfirmedIntegrationEvent @event)
        {
            _logger.LogInformation(" [x] OrderStatusChangedToStockConfirmedIntegrationEventHandler: Handling integration event..."); 

            if(isPaymentSuccessful)
            {
                _logger.LogInformation(" [x] PAYMENT: WAITING RESPONSE FROM PAYMENT VENDOR...");
                Thread.Sleep(5000);
                _logger.LogInformation(" [x] PAYMENT: PAYMENT IS SUCCESSFUL!");

                var integrationEvent = new OrderPaymentSucceededIntegrationEvent(orderId: @event.OrderId);
                _eventBus.Publish(integrationEvent);
            }

            else
            {
                _logger.LogInformation(" [x] PAYMENT: WAITING RESPONSE FROM PAYMENT VENDOR...");
                Thread.Sleep(5000);
                _logger.LogError(" [x] PAYMENT: PAYMENT METHOD IS REJECTED!");

                var integrationEvent = new OrderPaymentFailedIntegrationEvent(orderId: @event.OrderId, reason: "Insufficient balance");
                _eventBus.Publish(integrationEvent);
            }
            




            return null;
        }
    }
}