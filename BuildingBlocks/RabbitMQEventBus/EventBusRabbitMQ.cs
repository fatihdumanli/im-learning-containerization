using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using EventBus;
using EventBus.Abstractions;
using EventBus.Events;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQEventBus
{
    public class EventBusRabbitMQ : IEventBus
    {
        private readonly string rabbitMQ_hostName = "localhost";
        private readonly string exchange_Name = "eshop_exchange";
        //Hep aynı consumerchannel'ı kullanmak durumundayız.
        //Cunku dispose ediliyor.
    
        private IModel _consumerChannel;
        private string _queueName;
        private readonly ILogger<EventBusRabbitMQ> _logger;
        private readonly IEventBusSubscriptionManager _subsManager;

        private readonly ILifetimeScope _autofac;
        private readonly string AUTOFAC_SCOPE_NAME = "eshop_event_bus";

        public EventBusRabbitMQ(string queueName, 
            IEventBusSubscriptionManager subsManager,
            ILogger<EventBusRabbitMQ> logger = null) 
        {
            this._logger = logger;
            this._subsManager = subsManager;
        
            if(logger == null)
                this._logger = NullLogger<EventBusRabbitMQ>.Instance;

            _logger.LogInformation(" [X] INFO FROM EventBusRabbitMQ:" +
                 "Created new EventBusRabbitMQ instance. for queue: {0}", queueName);

            _queueName = queueName;
        }
        public void Publish(IntegrationEvent @event)
        {
            _logger.LogInformation(" [X] INFO FROM EventBusRabbitMQ:" +
                            "Published new event {0}", JsonConvert.SerializeObject(@event));   

            _logger.LogInformation(" [X] Routing key of most recenlty published message: {0}", @event.GetType().Name);   

            var factory = new ConnectionFactory() { HostName = rabbitMQ_hostName };

            using(var connection = factory.CreateConnection())
            {
                using(var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchange: this.exchange_Name, ExchangeType.Direct);

                    var message = JsonConvert.SerializeObject(@event);
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(this.exchange_Name, @event.GetType().Name, basicProperties: null,
                     body);
                }
            }
        }

        public void Subscribe<TE, TH>()
            where TE : IntegrationEvent
            where TH : IIntegrationEventHandler<TE>
        {
             string routingKey = typeof(TE).Name;

            _subsManager.AddSubscription<TE, TH>();
            
            var handler = _subsManager.GetHandlerType(routingKey);

            //internalSubscription, bu routing Key'deki mesajlarla ilgileniyorum, queue'ma gelsin.
            this._consumerChannel.QueueBind(this._queueName, this.exchange_Name, routingKey, null);

            _logger.LogInformation(" [x] Queue {0} subscribed for routingKey: {1} with eventHandler: {2}",
                 this._queueName, routingKey, typeof(TH).Name);
        }
    
         public void StartConsuming()
        {
            var factory = new ConnectionFactory() { HostName = rabbitMQ_hostName };
            var connection = factory.CreateConnection();
            this._consumerChannel = connection.CreateModel();

            this._consumerChannel.ExchangeDeclare(exchange: this.exchange_Name, ExchangeType.Direct);

            this._consumerChannel.QueueDeclare(this._queueName, false, false, false, null);
            
            var consumer = new EventingBasicConsumer(this._consumerChannel);

            consumer.Received += Message_Received;

            this._consumerChannel.BasicConsume(this._queueName, true, consumer);
        }

        private void Message_Received(object sender, BasicDeliverEventArgs e)
        {
            _logger.LogInformation(" [x] Message received: {0} to queue {1}.",
                 e.RoutingKey, _queueName);

            var isSubscribedToEvent =_subsManager.HasSubscription(e.RoutingKey);

            //Bir string var, bu stringe göre bir class'ın bir methodunu çağırmak istiyoruz.
            var body = e.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            _logger.LogInformation(" \t [.] Content of most recently message: {0}",
                message);

            var integrationEvent = 
            JsonConvert.DeserializeObject(message, _subsManager.GetEventType(e.RoutingKey));     
            _logger.LogInformation(" [x] Deserialized integrationEvent: {0}", integrationEvent);

            Type type = _subsManager.GetHandlerType(e.RoutingKey);

            
            var instance = Activator.CreateInstance(type);
            _logger.LogInformation(" [x] Created instance: {0}", instance);

            //var concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(type);    
            type.GetMethod("Handle").Invoke(instance, new object[] { integrationEvent });       
        }



    }
}