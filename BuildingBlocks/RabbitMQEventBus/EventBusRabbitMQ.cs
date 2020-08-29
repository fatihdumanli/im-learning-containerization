using System.Text;
using EventBus.Abstractions;
using EventBus.Events;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQEventBus
{
    public class EventBusRabbitMQ : IEventBus
    {
        private readonly string rabbitMQ_hostName = "localhost";
        private readonly string exchange_Name = "eshop_exchange";
        private IModel _consumerChannel;
        private string _queueName;


        public EventBusRabbitMQ(string queueName)
        {
            _consumerChannel = CreateConsumerChannel();
            _queueName = queueName;
        }

        private IModel CreateConsumerChannel()
        {
            var factory = new ConnectionFactory() { HostName = rabbitMQ_hostName };

            var conn = factory.CreateConnection();

            var channel = conn.CreateModel();
            
            channel.ExchangeDeclare(exchange: this.exchange_Name, ExchangeType.Fanout);

            channel.QueueDeclare(queue: this._queueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            return channel;
        }
        public void Publish(IntegrationEvent @event)
        {
            var factory = new ConnectionFactory() { HostName = this.rabbitMQ_hostName };

            using(var connection = factory.CreateConnection())
            {
                using(var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchange: this.exchange_Name, ExchangeType.Fanout);

                    var message = JsonConvert.SerializeObject(@event);
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(this.exchange_Name, @event.GetType().Name, basicProperties: null,
                     body);
                }
            }
        }

        public void Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {

                var factory = new ConnectionFactory() { HostName = rabbitMQ_hostName };

                using(var connection = factory.CreateConnection())
                {
                    this._consumerChannel.QueueBind(queue: _queueName,
                        exchange: this.exchange_Name, routingKey: null);
                }

                //start basic consumer.

                var consumer = new EventingBasicConsumer(_consumerChannel);
                
                _consumerChannel.BasicConsume(queue: this._queueName,
                    autoAck: false,
                    consumer: consumer);

                consumer.Received += (model, ea) => {
                    
                    var x = 2;
                };

        }
    }
}