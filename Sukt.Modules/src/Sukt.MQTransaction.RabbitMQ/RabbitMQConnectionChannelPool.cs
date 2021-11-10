using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace Sukt.MQTransaction.RabbitMQ
{
    public class RabbitMQConnectionChannelPool : IRabbitMQConnectionChannelPool
    {
        private const int DefaultPoolSize = 15;
        public string Host { get; }
        private IConnection _connection;
        private static readonly object connctionlock = new object();
        private readonly ILogger<RabbitMQConnectionChannelPool> _logger;
        private readonly ConcurrentQueue<IModel> _queue;
        private readonly RabbiMQOptions _options;
        private int _count;
        private int _maxsize;
        public RabbitMQConnectionChannelPool(ILogger<RabbitMQConnectionChannelPool> logger, IOptions<RabbiMQOptions> options)
        {
            _queue = new ConcurrentQueue<IModel>();
            _logger = logger;
            _maxsize = DefaultPoolSize;
            _options = options.Value;
        }
        public void Dispose()
        {
            _maxsize = 0;
            while (_queue.TryDequeue(out var conetxt))
            {
                conetxt.Dispose();
            }
            _connection?.Dispose();
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

        IModel IRabbitMQConnectionChannelPool.Rent()
        {
            //lock (connctionlock)
            //{
                while (_count>_maxsize)
                {
                    Thread.SpinWait(1);
                }
                return Rent();
            //}
        }
        /// <summary>
        /// 返还Model
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        bool IRabbitMQConnectionChannelPool.Return(IModel context)
        {
            if(Interlocked.Increment(ref _count)<=_maxsize&& context.IsOpen)
            {
                _queue.Enqueue(context);
                return true;
            }
            context.Dispose();
            Interlocked.Decrement(ref _count);
            Debug.Assert(_maxsize == 0 || _queue.Count <= _maxsize);
            return false;
        }
        /// <summary>
        /// 借用Model
        /// </summary>
        /// <returns></returns>
        public virtual IModel Rent()
        {
            if(_queue.TryDequeue(out var model))
            {
                Interlocked.Decrement(ref _count);
                Debug.Assert(_count >= 0);
                return model;
            }
            try
            {
                model = GetConnection().CreateModel();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "RabbitMQ 通道创建失败！");
                throw;
            }
            return model;
        }
        public virtual IModel CreateModel()
        {
            if (_connection != null && _connection.IsOpen)
            {
                return _connection.CreateModel();
            }
            throw new InvalidOperationException("No RabbitMQ connections are available to perform this action");
        }
    }
}
