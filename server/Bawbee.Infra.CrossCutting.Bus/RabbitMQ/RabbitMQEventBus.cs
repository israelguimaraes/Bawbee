using Bawbee.Domain.Core.Bus;
using Bawbee.Domain.Core.Events;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bawbee.Infra.CrossCutting.Bus.RabbitMQ
{
    public class RabbitMQEventBus : IEventBus
    {
        public RabbitMQEventBus()
        {

        }

        public Task Publish(Event @event)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
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
            var factory = new ConnectionFactory { HostName = "localhost", DispatchConsumersAsync = true };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            var typeEvent = typeof(T);
            var eventName = typeEvent.Name;

            channel.QueueDeclare(
                queue: eventName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.Received += Consumer_Received;

            channel.BasicConsume(
                queue: eventName,
                autoAck: true,
                consumer);

        }

        private async Task Consumer_Received(object sender, BasicDeliverEventArgs eventArgs)
        {
            var eventName = eventArgs.RoutingKey;
            var message = Encoding.UTF8.GetString(eventArgs.Body.ToArray());

            try
            {
                await ProcessMessage(eventName, message);
            }
            catch (Exception ex)
            {
                // TODO: ...
                throw;
            }
        }

        private Task ProcessMessage(string eventName, string message)
        {
            return Task.CompletedTask;
        }
    }
}
