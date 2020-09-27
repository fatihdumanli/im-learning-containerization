using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.API.Application.IntegrationEvents.Events;
using Ordering.API.Application.IntegrationEvents.IntegrationEventService;
using Ordering.Domain.AggregatesModel.BuyerAggregate;
using Ordering.Domain.AggregatesModel.OrderAggregate;
using Ordering.Domain.DomainEvents;

namespace Ordering.API.Application.DomainEventHandlers
{
    public class SendStockConfirmedIntegrationEventWhenStockConfirmed :
        INotificationHandler<OrderStatusChangedToStockConfirmedDomainEvent>
    {

        ILogger<SendStockConfirmedIntegrationEventWhenStockConfirmed> _logger;
        private IOrderRepository _orderRepository;
        private IBuyerRepository _buyerRepository;

        private IOrderingIntegrationEventService _orderingIntegrationEventService;

        public SendStockConfirmedIntegrationEventWhenStockConfirmed(
            IOrderRepository orderRepository,
            IBuyerRepository buyerRepository,
            IOrderingIntegrationEventService orderingIntegrationEventService,
            ILogger<SendStockConfirmedIntegrationEventWhenStockConfirmed> logger)
        {
            _logger = logger;      
            _orderRepository = orderRepository;  
            _buyerRepository = buyerRepository;
            _orderingIntegrationEventService = orderingIntegrationEventService;
        }


        public async Task Handle(OrderStatusChangedToStockConfirmedDomainEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(" [x] SendStockConfirmedIntegrationEventWhenStockConfirmed: " + 
                "Preparing integration event: OrderStatusChangedToStockConfirmedIntegrationEvent");
            
            var order = await _orderRepository.GetAsync(notification.OrderId);
            var buyer = await _buyerRepository.FindByIdAsync(Convert.ToInt32(order.GetBuyerId));

            var paymentMethod = buyer.PaymentMethods.First();
            
            var integrationEvent = new OrderStatusChangedToStockConfirmedIntegrationEvent(notification.OrderId, buyer: buyer.Name, 
                paymentMethod: paymentMethod);


            _logger.LogInformation(" [x] SendStockConfirmedIntegrationEventWhenStockConfirmed: " + 
                "Integration event is publishing through event bus. Order ID: {0}, Buyer: {1}, PaymentMethod: {2}",
                    order.Id, buyer.Name, paymentMethod.CardNumber);

            await _orderingIntegrationEventService.AddIntegrationEventToLog(integrationEvent, null);
            await _orderingIntegrationEventService.PublishEvent(integrationEvent);
        }
    }
}