using Bawbee.Domain.Core.Bus;
using RabbitMQ.Client;
using System;

namespace Bawbee.Infra.CrossCutting.Bus.RabbitMQ
{
    public class RabbitMQConnection : IEventBusConnection<IModel>, IDisposable
    {
        private ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _channel;

        public RabbitMQConnection()
        {

        }

        public void ConnectIfNecessary()
        {
            if (IsConnected())
                return;

            _connectionFactory = new ConnectionFactory { HostName = "localhost" };
            _connection = _connectionFactory.CreateConnection();
        }

        public bool IsConnected()
        {
            return _connection != null && _connection.IsOpen;
        }

        public IModel CreateChannel()
        {
            ConnectIfNecessary();

            return _connection.CreateModel();
        }

        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}
