using System.Linq;
using System.Threading.Tasks;
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
            _logger.LogInformation(string.Format("Order count: {0}",_context.Orders.Count()));
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
            return _context.Orders.Add(order).Entity;
        }

        public Task<Order> GetAsync(int orderId)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Order order)
        {
            throw new System.NotImplementedException();
        }
    }
}