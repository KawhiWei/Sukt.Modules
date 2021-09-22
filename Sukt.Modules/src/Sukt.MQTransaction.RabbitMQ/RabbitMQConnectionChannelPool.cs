using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.MQTransaction.RabbitMQ
{
    public class RabbitMQConnectionChannelPool : IRabbitMQConnectionChannelPool
    {
        public string Host { get; }
        private IConnection _connection;
        private static readonly object connctionlock = new object();
        private readonly ILogger<RabbitMQConnectionChannelPool> _logger;
        private readonly RabbiMQOptions _options;
        public RabbitMQConnectionChannelPool(ILogger<RabbitMQConnectionChannelPool> logger, IOptions<RabbiMQOptions> options)
        {
            _logger = logger;
            _options = options.Value;
        }
        public void Dispose()
        {
            
        }

        public IConnection GetConnection()
        {
            lock (connctionlock)
            {
                if(_connection!=null && _connection.IsOpen)
                {
                    return _connection;
                }
                _connection?.Dispose();
                _connection = CreateConnction(options: _options);
                _logger.LogInformation($"链接RabbitMQ成功！");
                    return _connection;
            }
        }
        private static IConnection CreateConnction(RabbiMQOptions options)
        {
            var factory = new ConnectionFactory
            {
                HostName = options.Host,
                UserName = options.UserName,
                Password = options.Password,
                Port = options.Port,
            };
            return factory.CreateConnection();

        }
    }
}
