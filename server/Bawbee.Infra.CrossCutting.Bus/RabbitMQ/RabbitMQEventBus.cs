using Bawbee.Domain.Core.Bus;
using Bawbee.Domain.Core.Events;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
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
        private readonly SubscriptionsManager _subscriptionsManager;
        private readonly IEventBusConnection<IModel> _busConnection;
        private readonly IServiceProvider _serviceProvider;

        public RabbitMQEventBus(IEventBusConnection<IModel> busConnection, IServiceProvider serviceProvider)
        {
            _busConnection = busConnection;
            _serviceProvider = serviceProvider;
            _subscriptionsManager = new SubscriptionsManager();
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
            var eventType = typeof(T);
            var eventName = eventType.Name;

            _subscriptionsManager.AddSubscriptionIfNotExists(eventName, eventType);

            var channel = _busConnection.CreateChannel();

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

        private async Task ProcessMessage(string eventName, string message)
        {
            var type = _subscriptionsManager.GetSubscriptionType(eventName);
            var @event = (IEvent)JsonConvert.DeserializeObject(message, type);

            using (var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetService<IMediator>();
                await mediator.Publish(@event);
            }
        }
    }
}
