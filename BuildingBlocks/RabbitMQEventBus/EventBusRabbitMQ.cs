using System.Text;
using EventBus.Abstractions;
using EventBus.Events;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace RabbitMQEventBus
{
    public class EventBusRabbitMQ : IEventBus
    {
        private readonly string rabbitMQ_hostName = "localhost";
        private readonly string exchange_Name = "eshop_exchange";


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
    }
}