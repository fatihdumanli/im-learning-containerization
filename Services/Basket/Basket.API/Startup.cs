using Autofac;
using Autofac.Extensions.DependencyInjection;
using Basket.API.Infrastructure.Repository;
using Basket.API.IntegrationEvents.EventHandling;
using Basket.API.IntegrationEvents.Events;
using Basket.API.Model;
using Basket.API.Services;
using EventBus;
using EventBus.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQEventBus;
using StackExchange.Redis;

namespace Basket.API
{
    public class Startup
    {
        public ILifetimeScope AutofacContainer { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
      
        public IServiceCollection AddRabbitMqConnection(IServiceCollection services)
        {
            services.AddSingleton<ConnectionMultiplexer>(sp =>
            {
                string connString = "basketdata, abortConnect=false";
                var configuration = ConfigurationOptions.Parse(connString, true);

                configuration.ResolveDns = true;

                return ConnectionMultiplexer.Connect(configuration);
            });
            return services;
        }
        public IServiceCollection AddLogging(IServiceCollection services)
        {
            services.AddLogging(config =>
            {
                config.AddDebug();
                config.AddConsole();
            })
            .Configure<LoggerFilterOptions>(o => {});
            return services;
        }
        public IServiceCollection AddSwaggerGen(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "eShop - Basket API",
                    Version = "v1",
                    Description = "The basket microservice HTTP API. This is a Data-Driven/CRUD microservice sample"
                });
            });

            return services;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
       
       
       public IServiceCollection AddEventBus(IServiceCollection services)
        {
            
            services.AddSingleton<IEventBusSubscriptionManager, InMemoryEventBusSubscriptionManager>(serviceProvider =>
            {
                var logger = serviceProvider.GetRequiredService<ILogger<InMemoryEventBusSubscriptionManager>>();
                return new InMemoryEventBusSubscriptionManager(logger);
            });

            services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp => {
                var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
                var subsManager = sp.GetRequiredService<IEventBusSubscriptionManager>();
                var lifeTimeScope = sp.GetRequiredService<ILifetimeScope>();
                return new EventBusRabbitMQ("Basket", subsManager, lifeTimeScope, logger);
            });
            
            
            return services;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddControllers();
            AddEventBus(services);
            AddSwaggerGen(services);
            AddLogging(services);
            AddRabbitMqConnection(services);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IBasketRepository, RedisBasketRepository>();
            services.AddTransient<IIdentityService, IdentityService>();            

       
        }


        public void ConfigureContainer(ContainerBuilder builder)
        {        
            builder.RegisterType<ProductPriceChangedIntegrationEventHandler>();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            ILoggerFactory loggerFactory)
        {
            this.AutofacContainer = app.ApplicationServices.GetAutofacRoot();

            loggerFactory.AddFile("C:\\logs\\1.txt");
            app.UseSwagger().UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1.0"));

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

            ConfigureEventBus(app);
        }

        //For registering 
        protected virtual void ConfigureEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.StartConsuming();  
            eventBus.Subscribe<ProductPriceChangedIntegrationEvent, ProductPriceChangedIntegrationEventHandler>();
        }
    }
}
