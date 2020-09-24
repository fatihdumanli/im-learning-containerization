using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Domain.AggregatesModel.OrderAggregate;
using Ordering.Domain.DomainEvents;

namespace Ordering.API.Application.DomainEventHandlers
{
    public class UpdateOrderWhenPaymentMethodValidated : INotificationHandler<PaymentMethodValidatedDomainEvent>
    {
        private IOrderRepository _orderRepository;
        private ILogger<UpdateOrderWhenPaymentMethodValidated> _logger;

        public UpdateOrderWhenPaymentMethodValidated(IOrderRepository orderRepository, ILogger<UpdateOrderWhenPaymentMethodValidated> logger)
        {
            _logger = logger;
            _orderRepository = orderRepository;   
            _logger.LogInformation(" [x] UpdateOrderWhenPaymentMethodValidated: Creating an instance of UpdateOrderWhenPaymentMethodValidated: {0}",
                this.GetHashCode());         
        }

        public async Task Handle(PaymentMethodValidatedDomainEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(" [x] UpdateOrderWhenPaymentMethodValidated.Handle(): Handling PaymentMethodValidated domain event...");

            try
            {
                var order = await _orderRepository.GetAsync(orderId: notification.OrderId);
                order.SetBuyerId(notification.Buyer.Id);
                order.SetPaymentMethodId(notification.PaymentMethod.Id);
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                throw e;
            }
           
            
        }
    }
}