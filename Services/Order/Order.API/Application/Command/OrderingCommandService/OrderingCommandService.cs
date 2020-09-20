using System;
using System.Threading.Tasks;
using DomainDispatching;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ordering.Infrastructure;

namespace Ordering.API.Application.Command.OrderingCommandService
{
    public class OrderingCommandService : IOrderingCommandService
    {
        private DomainDispatcher _domainDispatcher;
        private OrderingContext _context;
        private ILogger<OrderingCommandService> _logger;
        
        

        public OrderingCommandService(DomainDispatcher domainDispatcher, OrderingContext context, ILogger<OrderingCommandService> logger)
        {
            _domainDispatcher = domainDispatcher ?? throw new ArgumentNullException(nameof(domainDispatcher));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger;
        }


        public Task SendCommand(DomainDispatching.Commanding.Command command)
        {
            //Begin a transaction before sending a command to dispatcher

            var strategy = _context.Database.CreateExecutionStrategy();

            strategy.Execute(async () => {
                var transaction = await _context.BeginTransactionAsync();
                var currentTransaction = _context.GetCurrentTransaction();
                 _logger.LogInformation(" [x] OrderingCommandService.SendCommand(): Initiated new transaction: {0}", transaction.TransactionId);
                 _domainDispatcher.DispatchCommand(command);
            });                      

            return null;
        }
    }
}