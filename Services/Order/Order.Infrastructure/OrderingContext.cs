using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DomainDispatching;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Ordering.Domain.AggregatesModel.BuyerAggregate;
using Ordering.Domain.AggregatesModel.OrderAggregate;
using Ordering.Domain.SharedKernel;
using Ordering.Infrastructure.EntityConfigurations;

namespace Ordering.Infrastructure
{
    public class OrderingContext : DbContext, IUnitOfWork
    {
        private readonly ILogger<OrderingContext> _logger;
        public const string DEFAULT_SCHEMA = "ordering";
        private IMediator _mediator;
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<PaymentMethod> Payments { get; set; }
        public DbSet<Buyer> Buyers { get; set; }
        public DbSet<CardType> CardTypes { get; set; }
        public DbSet<OrderStatus> OrderStatus { get; set; }
        private IDbContextTransaction _currentTransaction;
        public OrderingContext(DbContextOptions<OrderingContext> options,
            ILogger<OrderingContext> logger,
            IMediator mediator) : base(options) 
        {             
            Guid objIdentifier = Guid.NewGuid();
            _logger = logger ?? throw new ArgumentNullException("Logger is null!");
            _logger.LogInformation(" [x] OrderContext: Creating an instance of OrderContext class. Guid: {0}", objIdentifier);
            _mediator = mediator ?? throw new ArgumentNullException("OrderingContext needs an Mediator object to publish domain events!");
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
          
    
            
           #region Publishing domain events.
            var entries = this.ChangeTracker.Entries();
            var domainEntities = this.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any()); 
                
                     
            _logger.LogInformation(" [x] OrderingContext.SaveEntitiesAsync(): {0} entity found in the OrderingContext.ChangeTracker.",
             domainEntities.Count());           
            
            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            _logger.LogInformation(" [x] OrderingContext.SaveEntitiesAsync(): {0} domain events found to publish.", domainEvents.Count);

            /*
             * Önce clear etmek gerek,
             * Çünkü domain event handler lar buraya çağırıyor, clear edilmeden yeni eventlar yayınlanıyor,
             * Bu da deadlock a, sonrasında stackOverflow'a yol açıyor.              
            */
            domainEntities.ToList()
             .ForEach(entity => entity.Entity.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
            {
                _logger.LogInformation("[x] OrderingContext.SaveEntitiesAsync(): Publishing domain event: {0}", domainEvent.GetType().Name);
                await _mediator.Publish(domainEvent);
            }
            
        
                
            #endregion
            // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
            // performed through the DbContext will be committed
            _logger.LogInformation(" [x] OrderingContext.SaveEntitiesAsync(): Entities are being saved to persistance.");
            var result = await base.SaveChangesAsync();
            _logger.LogInformation(string.Format(" [x] OrderingContext.SaveEntitiesAsync(): Entities are persisted successfully. Persisted entity count: {0}", result));
            
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

        public override int SaveChanges()
        {
            _logger.LogWarning(" [x] OrderingContext.SaveChangesAsync(): Current transaction: {0}", this.Database.CurrentTransaction);
            return base.SaveChanges();
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

            return new OrderingContext(optionsBuilder.Options, null, null);
        }

    }
}