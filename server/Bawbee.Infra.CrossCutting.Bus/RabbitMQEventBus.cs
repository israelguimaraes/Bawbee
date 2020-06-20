using Bawbee.Domain.Core.Bus;
using Bawbee.Domain.Core.Events;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace Bawbee.Infra.CrossCutting.Bus
{
    public class RabbitMQEventBus : IEventBus
    {
        public const string QUEUE_BAWBEE_EVENTS = "queue_bawbee_events";

        public void Publish(Event @event)
        {




            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(
                    queue: QUEUE_BAWBEE_EVENTS,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                var message = JsonConvert.SerializeObject(@event);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(
                    exchange: "",
                    routingKey: "hello",
                    basicProperties: null,
                    body: body);

                // TODO: log event?
            }
        }
    }
}
