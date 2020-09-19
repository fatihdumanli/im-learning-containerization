using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ordering.Domain.AggregatesModel.BuyerAggregate;
using Ordering.Domain.SharedKernel;

namespace Ordering.Infrastructure.Repositories
{
    public class BuyerRepository : IBuyerRepository
    {

        private readonly OrderingContext _context;
        private ILogger<BuyerRepository> _logger;

        public BuyerRepository(OrderingContext context, ILogger<BuyerRepository> logger)
        {
            _logger = logger;
            _context = context ?? throw new ArgumentNullException("BuyerRepository needs an OrderingContext object.");
            _logger.LogInformation(" [x] BuyerRepository: Creating an instance of BuyerRepository.");
        }

        public IUnitOfWork UnitOfWork 
        {
            get 
            {
                return _context;
            }
        }

        public Buyer Add(Buyer buyer)
        {
            if(buyer.IsTransient())
            {
                return _context.Buyers.Add(buyer).Entity;
            }

            else 
            {
                return buyer;
            }
        }

        public async Task<Buyer> FindAsync(string buyerIdentityGuid)
        {
            var buyer = await _context.Buyers
                //.Include(i => i.PaymentMethods)
                .Where(b => b.Name == buyerIdentityGuid)
                .SingleOrDefaultAsync();
            
            return buyer;
        }

        public Buyer FindByNameAsync(string name)
        {
            var buyer = _context.Buyers
                .Where(b => b.Name == name)
                .Include(b => b.PaymentMethods)
                .SingleOrDefault();

            return buyer;        
        }

        public Buyer Update(Buyer buyer)
        {
            return _context.Buyers.Update(buyer).Entity;
        }
    }
}