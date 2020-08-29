using System.Collections.Generic;
using System.Text;
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
        private List<string> _subscriptions;

        public EventBusRabbitMQ(string queueName, ILogger<EventBusRabbitMQ> logger = null) 
        {
            this._logger = logger;

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

       
        private void Message_Received(object sender, BasicDeliverEventArgs e)
        {
            _logger.LogInformation(" [x] Message received");
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

            this._consumerChannel.BasicConsume(this._queueName, false, consumer);
        }

        public void Subscribe(string routingKey)
        {   
            //Ben bu routingKey'i de bu exchange'e bağlıyorum.
            this._consumerChannel.QueueBind(this._queueName, this.exchange_Name, routingKey, null);
            this._subscriptions.Add(routingKey);
        }
    }
}