using System.Threading.Tasks;
using Ordering.Domain.AggregatesModel.OrderAggregate;
using Ordering.Domain.SharedKernel;

namespace Ordering.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public IUnitOfWork UnitOfWork 
        {
            get 
            {
                return null;
            }
        }

        public Order Add(Order order)
        {
            throw new System.NotImplementedException();
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