using Bawbee.Domain.Core.Bus;
using Bawbee.Domain.Core.Events;
using MediatR;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bawbee.Infra.CrossCutting.Bus
{
    public class RabbitMQEventBus : IEventBus
    {
        public const string QUEUE_BAWBEE_EVENTS = "queue_bawbee_events";

        private static IDictionary<string, Type> _listEventTypes;
        private readonly IMediator _mediator;

        public RabbitMQEventBus(IMediator mediator)
        {
            _mediator = mediator;
            _listEventTypes = new Dictionary<string, Type>();
        }

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
                    routingKey: QUEUE_BAWBEE_EVENTS,
                    basicProperties: null,
                    body: body);
            }

            // TODO: log event?
        }

        public void Subscribe<T>() where T : Event
        {
            var typeEvent = typeof(T);
            var eventName = typeEvent.Name;

            if (!_listEventTypes.ContainsKey(eventName))
                _listEventTypes.Add(eventName, typeEvent);

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

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += async (model, args) =>
                {
                    var eventName = args.RoutingKey;
                    var message = Encoding.UTF8.GetString(args.Body.ToArray());

                    var type = _listEventTypes[eventName];
                    var @event = (Event)JsonConvert.DeserializeObject(message, type);

                    await _mediator.Publish(@event);
                };
                
                channel.BasicConsume(queue: QUEUE_BAWBEE_EVENTS,
                     autoAck: true,
                     consumer: consumer);
            }

            // TODO: log event?
        }
    }
}
