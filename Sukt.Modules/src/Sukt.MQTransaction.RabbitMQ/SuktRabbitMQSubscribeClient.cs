using JetBrains.Annotations;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Sukt.Module.Core.Extensions;
using Sukt.MQTransaction.Factory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Sukt.MQTransaction.RabbitMQ
{
    internal sealed class SuktRabbitMQSubscribeClient : ISuktSubscribeClient
    {
        /// <summary>
        /// 添加一部锁，每次只允许一个线程进入
        /// </summary>
        private readonly SemaphoreSlim _semaphoreSlimLock = new SemaphoreSlim(initialCount: 1, maxCount: 1);
        private readonly RabbiMQOptions _options;
        private readonly IRabbitMQConnectionChannelPool _connectionChannelPool;
        private IModel _rabbitchannelmodel;
        private IConnection _connection;

        public event EventHandler<MessageCarrier> OnMessageReceived;

        //public event EventHandler<TransportMessage> OnMessageReceived;
        public SuktRabbitMQSubscribeClient(IOptions<RabbiMQOptions> options, IRabbitMQConnectionChannelPool connectionChannelPool)
        {
            _options = options?.Value;
            _connectionChannelPool = connectionChannelPool;
        }
        public void SubscribeQueueBind(string exchange, string topicOrRoutingKeyName, string queue, string exchangeType = "topic")
        {
            if (exchange.IsNullOrEmpty() || topicOrRoutingKeyName.IsNullOrEmpty())
            {
                throw new ArgumentNullException($"{nameof(exchange)}--------------{nameof(topicOrRoutingKeyName)}");
            }
            Connection(exchange: exchange, queue: queue, exchangeType: exchangeType);
            _rabbitchannelmodel.QueueBind(queue, exchange, topicOrRoutingKeyName);
        }
        public void Connection(string exchange, string queue, string exchangeType = "topic")
        {
            if (_connection != null)
            {
                return;
            }
            _semaphoreSlimLock.Wait();
            try
            {
                if (_connection == null)
                {
                    _connection = _connectionChannelPool.GetConnection();
                    _rabbitchannelmodel = _connection.CreateModel();
                    _rabbitchannelmodel.ExchangeDeclare(exchange, exchangeType, true);
                    _rabbitchannelmodel.QueueDeclare(queue, durable: true, exclusive: false, autoDelete: false);
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="exchange"></param>
        /// <param name="topicOrRoutingKeyName"></param>
        /// <param name="queue"></param>
        /// <param name="timedelay"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="exchangeType"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void SubscribeListening(string exchange, string topicOrRoutingKeyName, string queue, TimeSpan timedelay, CancellationToken cancellationToken, string exchangeType = "topic")
        {
            Connection(exchange, queue, exchangeType);
            var subscribe = new EventingBasicConsumer(_rabbitchannelmodel);
            subscribe.Received += OnConsumerReceived;

            _rabbitchannelmodel.BasicConsume(queue, false, subscribe);
            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();
                cancellationToken.WaitHandle.WaitOne(timedelay);
            }
        }
        private void OnConsumerReceived(object sender, BasicDeliverEventArgs e)
        {
            var headers = new Dictionary<string, string>();
            if (e.BasicProperties.Headers != null)
            {
                foreach (var header in e.BasicProperties.Headers)
                {
                    headers.Add(header.Key, header.Value == null ? null : Encoding.UTF8.GetString((byte[])header.Value));
                }
            }

            //headers.Add(Headers.Group, _queueName);
            var message = new MessageCarrier(headers, e.Body.ToArray());
            OnMessageReceived?.Invoke(e.DeliveryTag, message);
        }
        public void Dispose()
        {
            _rabbitchannelmodel?.Dispose();
        }

        public void Commit([NotNull] object sender)
        {
            if (_rabbitchannelmodel.IsOpen)
            {
                _rabbitchannelmodel.BasicAck((ulong)sender, false);
            }
        }

        public void Reject(object sender)
        {
            if (_rabbitchannelmodel.IsOpen)
            {
                _rabbitchannelmodel.BasicReject((ulong)sender, true);
            }
        }
    }
}
