using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.MQTransaction.RabbitMQ
{
    public class RabbitMQConnectionChannelPool : IRabbitMQConnectionChannelPool
    {
        public string Host => throw new NotImplementedException();

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IConnection GetConnection()
        {
            throw new NotImplementedException();
        }
    }
}
