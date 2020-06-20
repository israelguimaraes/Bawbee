using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bawbee.Infra.CrossCutting.Bus.RabbitMQ
{
    public class RabbitMQConnection : IRabbitMQConnection, IDisposable
    {
        private ConnectionFactory _connectionFactory;
        private IConnection _connection;

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
            return false;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
