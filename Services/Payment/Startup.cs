using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using EventBus;
using EventBus.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Payment.IntegrationEvents;
using RabbitMQEventBus;

namespace Payment
{
    public class Startup
    {
        public ILifetimeScope AutofacContainer { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.


        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<OrderStatusChangedToStockConfirmedIntegrationEventHandler>();
        
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

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
                return new EventBusRabbitMQ("Payment", subsManager: subsManager, autoFac: autoFac, rabbitMqServer: rabbitMqEndpoint, logger: logger);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            this.AutofacContainer = app.ApplicationServices.GetAutofacRoot();


            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.StartConsuming();
            eventBus.Subscribe<OrderStatusChangedToStockConfirmedIntegrationEvent, OrderStatusChangedToStockConfirmedIntegrationEventHandler>();
      
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
