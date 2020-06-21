using Bawbee.Domain.Core.Bus;
using Bawbee.Domain.Core.Events;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
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

        private readonly IEventBusConnection<IModel> _eventBusConnection;
        private readonly IServiceProvider _serviceProvider;

        public RabbitMQEventBus(IEventBusConnection<IModel> eventBusConnection, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _eventBusConnection = eventBusConnection;
            _listEventTypes = new Dictionary<string, Type>();
            
            CreateConsumeChannel();
        }

        public async Task<TResponse> SendCommand<TResponse>(IRequest<TResponse> command, CancellationToken cancellationToken = default)
        {
            var mediator = _serviceProvider.GetService<IMediator>();
            return await mediator.Send(command);


            /*using (var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetService<IMediator>();
                return await mediator.Send(command);
            }*/
        }

        public async Task Publish(Event @event)
        {
            if (@event.IsDomainNotification())
            {
                var mediator = _serviceProvider.GetService<IMediator>();
                await mediator.Publish(@event);

                /*

                using (var scope = _serviceProvider.CreateScope())
                {
                    //var mediator = scope.ServiceProvider.GetService<IMediator>();
                    //await mediator.Publish(@event);
                }*/
            }
            else
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
        }

        public void CreateConsumeChannel()
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

                using (var scope = _serviceProvider.CreateScope())
                {
                    var mediator = scope.ServiceProvider.GetService<IMediator>();
                    await mediator.Publish(@event);
                }

                channel.BasicAck(args.DeliveryTag, multiple: false);
                // TODO: log event?
            };

            channel.BasicConsume(
                queue: RabbitMQConfig.QUEUE_EVENTS_NAME,
                autoAck: true,
                consumer: consumer);
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
