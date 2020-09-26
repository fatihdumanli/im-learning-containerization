using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.API.Application.IntegrationEvents.Events;
using Ordering.API.Application.IntegrationEvents.IntegrationEventService;
using Ordering.Domain.AggregatesModel.OrderAggregate;

namespace Ordering.API.Application.Command
{
    public class SetOrderStatusAwaitingStockValidationCommandHandler : IRequestHandler<SetOrderStatusAwaitingStockValidationCommand, bool>
    {
        private IOrderRepository _orderRepository;
        private ILogger<SetOrderStatusAwaitingStockValidationCommandHandler> _logger;
        private IOrderingIntegrationEventService _integrationEventService;               

        public SetOrderStatusAwaitingStockValidationCommandHandler(IOrderRepository orderRepository, 
            IOrderingIntegrationEventService orderingIntegrationEventService,
            ILogger<SetOrderStatusAwaitingStockValidationCommandHandler> logger)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException("OrderRepository was null.");
            _integrationEventService = orderingIntegrationEventService ?? throw new ArgumentNullException("Ordering integration event service was null.");
            _logger = logger;
        }

        public async Task<bool> Handle(SetOrderStatusAwaitingStockValidationCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(" [x] SetOrderStatusAwaitingValidationCommandHandler.Handle(): Handling SetOrderStatusAwaitingValidationCommand...");
            
            var orderToUpdate = await _orderRepository.GetAsync(request.OrderId);

            if(orderToUpdate == null)
            {
                _logger.LogError(" [x] SetOrderStatusAwaitingValidationCommandHandler.Handle(): Order is not found, command will not be executed.");
                return false;
            }

            //Domain layer
            orderToUpdate.SetStatusAwaitingStockValidation();   
            _logger.LogInformation(" [x] ({0}) Order status transitioned SUBMITTED -----> AWAITINGVALIDATION", request.OrderId);
    
            //Infrastructure layer.
            return await _orderRepository.UnitOfWork.SaveEntitiesAsync();         
        }
    }
}