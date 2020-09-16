using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Storage;
using Ordering.Domain.AggregatesModel.BuyerAggregate;
using Ordering.Domain.AggregatesModel.OrderAggregate;
using Ordering.Domain.SharedKernel;
using Ordering.Infrastructure.EntityConfigurations;

namespace Ordering.Infrastructure
{
    public class OrderingContext : DbContext, IUnitOfWork
    {

        public const string DEFAULT_SCHEMA = "ordering";
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<PaymentMethod> Payments { get; set; }
        public DbSet<Buyer> Buyers { get; set; }
        public DbSet<CardType> CardTypes { get; set; }
        public DbSet<OrderStatus> OrderStatus { get; set; }
        private IDbContextTransaction _currentTransaction;
        public OrderingContext(DbContextOptions<OrderingContext> options) : base(options) 
        {             
            System.Diagnostics.Debug.WriteLine("options: " + options);
        }

        public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;
        public bool HasActiveTransaction => _currentTransaction != null;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrderEntityTypeConfiguration());   
            modelBuilder.ApplyConfiguration(new OrderItemEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OrderStatusEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new BuyerEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentMethodEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CardTypeEntityTypeConfiguration());

            modelBuilder.Entity<OrderStatus>().HasData(new OrderStatus(1, "Submitted"));
            modelBuilder.Entity<OrderStatus>().HasData(new OrderStatus(2, "AwaitingValidation"));
            modelBuilder.Entity<OrderStatus>().HasData(new OrderStatus(3, "StockConfirmed"));
            modelBuilder.Entity<OrderStatus>().HasData(new OrderStatus(4, "Paid"));
            modelBuilder.Entity<OrderStatus>().HasData(new OrderStatus(5, "Shipped"));
            modelBuilder.Entity<OrderStatus>().HasData(new OrderStatus(6, "Cancelled"));

            modelBuilder.Entity<CardType>().HasData(new CardType(1, "Amex"));
            modelBuilder.Entity<CardType>().HasData(new CardType(2, "Visa"));
            modelBuilder.Entity<CardType>().HasData(new CardType(3, "MasterCard"));            
        }

        //Entity'ler kaydedilirken tüm domain eventlar mediator aracılığıyla publish ediliyor.
        public async Task<bool> SaveEntitiesAsync()
        {
            // Dispatch Domain Events collection. 
            // Choices:
            // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
            // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
            // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
            // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
            
            //await _mediator.DispatchDomainEventsAsync(this);

            // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
            // performed through the DbContext will be committed
            var result = await base.SaveChangesAsync();

            return true;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if(_currentTransaction != null) return null;

            _currentTransaction = await Database.BeginTransactionAsync(System.Data.IsolationLevel.ReadCommitted);

            return _currentTransaction;
        }

        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            if(transaction == null) throw new ArgumentNullException(nameof(transaction));
            if(transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

            try 
            {
                await SaveChangesAsync();
                transaction.Commit();
            } 
            
            catch 
            {
                this.RollbackTransaction();
                throw;
            } 
            
            finally 
            {
                if(_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }


        public void RollbackTransaction() 
        {
            try
            {
                _currentTransaction.Rollback();
            }

            finally
            {
                if(_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }


    public class OrderingContextDesignFactory : IDesignTimeDbContextFactory<OrderingContext>
    {
        public OrderingContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<OrderingContext>()
                .UseSqlServer("Server=localhost,5433;Initial Catalog=OrderingService;User Id=sa;Password=Pass@word;TrustServerCertificate=True;Connection Timeout=5;");

            return new OrderingContext(optionsBuilder.Options);
        }

    }
}