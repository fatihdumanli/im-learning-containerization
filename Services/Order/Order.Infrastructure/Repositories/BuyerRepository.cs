using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ordering.Domain.AggregatesModel.BuyerAggregate;
using Ordering.Domain.SharedKernel;

namespace Ordering.Infrastructure.Repositories
{
    public class BuyerRepository : IBuyerRepository
    {

        private readonly OrderingContext _context;


        public BuyerRepository(OrderingContext context)
        {
            _context = context ?? throw new ArgumentNullException("BuyerRepository needs an OrderingContext object.");
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
                .Where(b => b.IdentityGuid == buyerIdentityGuid)
                .SingleOrDefaultAsync();
            
            return buyer;
        }

        public async Task<Buyer> FindByIdAsync(string id)
        {
            var buyer = await _context.Buyers
                .Where(b => b.Id == int.Parse(id))
                .SingleOrDefaultAsync();

            return buyer;
        }

        public Buyer Update(Buyer buyer)
        {
            return _context.Buyers.Update(buyer).Entity;
        }
    }
}