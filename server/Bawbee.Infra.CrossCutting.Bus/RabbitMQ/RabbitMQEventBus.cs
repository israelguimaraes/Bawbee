using Bawbee.Domain.Core.Bus;
using Bawbee.Domain.Core.Events;
using MediatR;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bawbee.Infra.CrossCutting.Bus.RabbitMQ
{
    public class RabbitMQEventBus : IEventBus, IDisposable
    {
        private static IDictionary<string, Type> _listEventTypes;

        private readonly IEventBusConnection<IModel> _eventBusConnection;
        private readonly IMediator _mediator;

        public RabbitMQEventBus(IEventBusConnection<IModel> eventBusConnection, IMediator mediator)
        {
            _eventBusConnection = eventBusConnection;
            _mediator = mediator;
            _listEventTypes = new Dictionary<string, Type>();
        }

        public void Publish(Event @event)
        {
            _eventBusConnection.ConnectIfNecessary();

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

        public void GetConsumerChannel()
        {
            _eventBusConnection.ConnectIfNecessary();
            
            var channel = _eventBusConnection.CreateChannel();

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
                var @event = (Event)JsonConvert.DeserializeObject(message, type);

                await _mediator.Publish(@event);

                // TODO: log event?
            };

            channel.BasicConsume(
                queue: RabbitMQConfig.QUEUE_EVENTS_NAME,
                autoAck: true,
                consumer: consumer);
        }

        public void Dispose()
        {
            _listEventTypes.Clear();
        }
    }
}
