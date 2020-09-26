using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ordering.Domain.AggregatesModel.OrderAggregate;
using Ordering.Domain.SharedKernel;

namespace Ordering.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private ILogger<OrderRepository> _logger;
        private readonly OrderingContext _context;

        public OrderRepository(OrderingContext context, ILogger<OrderRepository> logger)
        {
            this._context = context;
            this._logger = logger;
            
            _logger.LogInformation("[x] OrderRepository: Creating an OrderRepository instance.");           
        }

        public IUnitOfWork UnitOfWork 
        {
            get 
            {
                return _context;
            }
        }

        public Order Add(Order order)
        {
            _logger.LogInformation(" [x] OrderRepository.Add(): Order item is being added to Order DbSet.");
             var entity = _context.Orders.Add(order).Entity;
            return entity;
        }

        public async Task<Order> GetAsync(int orderId)
        {
            var order = await _context.Orders
                .Include(x => x.Address)
                .FirstOrDefaultAsync(o => o.Id == orderId);   

            if(order == null)
            {
                   order = _context
                            .Orders
                            .Local
                            .FirstOrDefault(o => o.Id == orderId);
            }
                        
            return order;
        }

        public void Update(Order order)
        {
            throw new System.NotImplementedException();
        }
    }
}