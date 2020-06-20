using Bawbee.Domain.Core.Bus;
using Bawbee.Domain.Core.Commands;
using Bawbee.Domain.Core.Events;
using MediatR;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bawbee.Infra.CrossCutting.Bus.RabbitMQ
{
    public class RabbitMQEventBus : IEventBus, IDisposable
    {
        private static IDictionary<string, Type> _listEventTypes;
        private IModel _consumerChannel;

        private readonly IEventBusConnection<IModel> _eventBusConnection;
        private readonly IMediator _mediator;

        public RabbitMQEventBus(IEventBusConnection<IModel> eventBusConnection, IMediator mediator)
        {
            _eventBusConnection = eventBusConnection;
            _mediator = mediator;
            _listEventTypes = new Dictionary<string, Type>();
            _consumerChannel = GetConsumerChannel();
        }

        public async Task<TResponse> SendCommand<TResponse>(IRequest<TResponse> command, CancellationToken cancellationToken = default)
        {
            return await _mediator.Send(command);
        }

        public void Publish(Event @event)
        {
            var channel = _eventBusConnection.CreateChannel();

            channel.QueueDeclare(
                    queue: RabbitMQConfig.QUEUE_EVENTS_NAME,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

            var message = JsonConvert.SerializeObject(@event);
            var body = Encoding.UTF8.GetBytes(message);

            var eventName = @event.GetType().Name;

            channel.BasicPublish(
                exchange: RabbitMQConfig.BROKER_EVENTS_NAME,
                routingKey: eventName,
                basicProperties: null,
                body: body);

            // TODO: log event?
        }

        public IModel GetConsumerChannel()
        {
            var channel = _eventBusConnection.CreateChannel();

            channel.ExchangeDeclare(
                exchange: RabbitMQConfig.BROKER_EVENTS_NAME,
                type: "direct");

            channel.QueueDeclare(
                    queue: RabbitMQConfig.QUEUE_EVENTS_NAME,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, args) =>
            {
                var eventName = args.RoutingKey;
                var message = Encoding.UTF8.GetString(args.Body.ToArray());

                var type = _listEventTypes[eventName];
                var @event = (IEvent)JsonConvert.DeserializeObject(message, type);

                await _mediator.Publish(@event);

                channel.BasicAck(args.DeliveryTag, multiple: false);
                // TODO: log event?
            };

            channel.BasicConsume(
                queue: RabbitMQConfig.QUEUE_EVENTS_NAME,
                autoAck: true,
                consumer: consumer);

            return channel;
        }

        public void Subscribe<T>() where T : Event
        {
            var typeEvent = typeof(T);
            var eventName = typeEvent.Name;

            if (!_listEventTypes.ContainsKey(eventName))
                _listEventTypes.Add(eventName, typeEvent);

            var channel = _eventBusConnection.CreateChannel();

            channel.QueueBind(
                    queue: RabbitMQConfig.QUEUE_EVENTS_NAME,
                    exchange: RabbitMQConfig.BROKER_EVENTS_NAME,
                    routingKey: eventName);
        }

        public void Dispose()
        {
            _listEventTypes.Clear();
        }
    }
}
