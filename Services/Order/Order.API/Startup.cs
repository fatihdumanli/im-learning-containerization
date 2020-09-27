using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using DomainDispatching;
using DomainDispatching.Commanding;
using DomainDispatching.DomainEvent;
using EventBus;
using EventBus.Abstractions;
using IntegrationEventLog;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ordering.API.Application.Command;
using Ordering.API.Application.DomainEventHandlers;
using Ordering.API.Application.IntegrationEvents.EventHandling;
using Ordering.API.Application.IntegrationEvents.Events;
using Ordering.API.Application.IntegrationEvents.IntegrationEventService;
using Ordering.Domain.AggregatesModel.BuyerAggregate;
using Ordering.Domain.AggregatesModel.OrderAggregate;
using Ordering.Domain.DomainEvents;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Repositories;
using RabbitMQEventBus;

namespace Ordering.API
{
    public class Startup
    {
        public ILifetimeScope AutofacContainer { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<GracePeriodConfirmedForOrderIntegrationEventHandler>();
            builder.RegisterType<UserCheckoutAcceptedIntegrationEventHandler>();
            builder.RegisterType<OrderStockConfirmedIntegrationEventHandler>();
            builder.RegisterType<OrderStockRejectedIntegrationEventHandler>();
            builder.RegisterType<OrderingIntegrationEventService>().As<IOrderingIntegrationEventService>();
            builder.RegisterType<Mediator>().As<IMediator>().InstancePerLifetimeScope();


            
             // Register all the Command classes (they implement IRequestHandler) in assembly holding the Commands
            builder.RegisterAssemblyTypes(typeof(CreateOrderCommand).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));

            // Register the DomainEventHandler classes (they implement INotificationHandler<>) in assembly holding the Domain Events
            builder.RegisterAssemblyTypes(typeof(ValidateOrAddBuyerWhenOrderStarted).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(INotificationHandler<>));

            builder.Register<ServiceFactory>(context =>
            {
                var c = context.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<OrderingContext>(options => {
                   options.UseSqlServer("Server=localhost,5433;Initial Catalog=OrderingService;User Id=sa;Password=Pass@word;TrustServerCertificate=True;Connection Timeout=10;",
                                         sqlServerOptionsAction: sqlOptions =>
                                         {
                                             sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                                             sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                                         });
            }, 
                ServiceLifetime.Scoped
            );

            services.AddDbContext<IntegrationEventLogContext>(options =>
            {
                options.UseSqlServer("Server=localhost,5433;Initial Catalog=OrderingService;User Id=sa;Password=Pass@word;TrustServerCertificate=True;Connection Timeout=10;",
                                     sqlServerOptionsAction: sqlOptions =>
                                     {
                                         sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                                         sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                                     });
            });

            
            services.AddControllers();
            services.AddScoped<IBuyerRepository, BuyerRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddSingleton<IEventBusSubscriptionManager, InMemoryEventBusSubscriptionManager>(serviceProvider =>
            {
                var logger = serviceProvider.GetRequiredService<ILogger<InMemoryEventBusSubscriptionManager>>();
                return new InMemoryEventBusSubscriptionManager(logger);
            });

            services.AddSingleton<IEventBus, EventBusRabbitMQ>(serviceProvider =>
            {
                var subsManager = serviceProvider.GetRequiredService<IEventBusSubscriptionManager>();
                var autoFac = serviceProvider.GetRequiredService<ILifetimeScope>();
                var rabbitMqEndpoint = Configuration["RabbitMQServer"];
                var logger = serviceProvider.GetRequiredService<ILogger<EventBusRabbitMQ>>();
                return new EventBusRabbitMQ("Order", subsManager: subsManager, autoFac: autoFac, rabbitMqServer: rabbitMqEndpoint, logger: logger);
            });


            services.AddLogging(config =>
            {
                config.AddDebug(); 
                config.AddConsole(); 
            })
           .Configure<LoggerFilterOptions>(options => {

           });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            this.AutofacContainer = app.ApplicationServices.GetAutofacRoot();

            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.StartConsuming();
            eventBus.Subscribe<UserCheckoutAcceptedIntegrationEvent, UserCheckoutAcceptedIntegrationEventHandler>();
            eventBus.Subscribe<GracePeriodConfirmedForOrderIntegrationEvent, GracePeriodConfirmedForOrderIntegrationEventHandler>();
            eventBus.Subscribe<OrderStockConfirmedIntegrationEvent, OrderStockConfirmedIntegrationEventHandler>();
            eventBus.Subscribe<OrderStockRejectedIntegrationEvent, OrderStockRejectedIntegrationEventHandler>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
