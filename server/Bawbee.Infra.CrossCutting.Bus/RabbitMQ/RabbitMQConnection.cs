using Bawbee.Core.Bus;
using RabbitMQ.Client;

namespace Bawbee.Infra.CrossCutting.Bus.RabbitMQ
{
    public class RabbitMQConnection : IEventBusConnection<IModel>
    {
        private IConnection _connection;
        private ConnectionFactory _factory;

        public RabbitMQConnection()
        {
            _factory = GetFactory();
            _connection = GetConnection();
        }

        private ConnectionFactory GetFactory()
        {
            _factory ??= new ConnectionFactory { HostName = "localhost", DispatchConsumersAsync = true };
            
            return _factory;
        }

        private IConnection GetConnection()
        {
            _connection ??= _factory.CreateConnection();
            
            return _connection;
        }

        public void TryConnectIfNecessary()
        {
            if (IsConnected())
                return;

            _connection = GetConnection();
        }

        public IModel CreateChannel()
        {
            return _connection.CreateModel();
        }

        public bool IsConnected()
        {
            return _connection != null && _connection.IsOpen;
        }
    }
}
