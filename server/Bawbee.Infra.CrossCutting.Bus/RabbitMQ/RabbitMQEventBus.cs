using Bawbee.Domain.Core.Bus;
using Bawbee.Domain.Core.Events;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Bawbee.Infra.CrossCutting.Bus.RabbitMQ
{
    public class RabbitMQEventBus : IEventBus
    {
        private readonly IEventBusConnection<IModel> _busConnection;

        public RabbitMQEventBus(IEventBusConnection<IModel> busConnection)
        {
            _busConnection = busConnection;
        }

        public Task Publish(Event @event)
        {
            _busConnection.TryConnectIfNecessary();

            using (var channel = _busConnection.CreateChannel())
            {
                var eventName = @event.GetType().Name;

                channel.QueueDeclare(
                    queue: eventName,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                var message = JsonConvert.SerializeObject(@event);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(
                    exchange: "",
                    routingKey: eventName,
                    basicProperties: null,
                    body: body);

                return Task.CompletedTask;
            }
        }

        public void Subscribe<T>() where T : Event
        {
            var channel = _busConnection.CreateChannel();

            var typeEvent = typeof(T);
            var eventName = typeEvent.Name;

            channel.QueueDeclare(
                queue: eventName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.Received += async (model, args) =>
            {
                try
                {
                    var eventName = args.RoutingKey;
                    var message = Encoding.UTF8.GetString(args.Body.ToArray());

                    await ProcessMessage(eventName, message);

                    channel.BasicAck(args.DeliveryTag, false);
                }
                catch (Exception ex)
                {
                    // TODO: log
                    channel.BasicNack(args.DeliveryTag, false, true);
                }
            };

            channel.BasicConsume(
                queue: eventName,
                autoAck: false,
                consumer);
        }

        private Task ProcessMessage(string eventName, string message)
        {
            return Task.CompletedTask;
        }
    }
}
