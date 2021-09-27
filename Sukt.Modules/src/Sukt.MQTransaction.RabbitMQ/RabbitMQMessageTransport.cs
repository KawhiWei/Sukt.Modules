using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using Sukt.Module.Core.Enums;
using Sukt.Module.Core.OperationResult;
using Sukt.MQTransaction.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.MQTransaction.RabbitMQ
{
    internal class RabbitMQMessageTransport: IMessageTransport
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<RabbitMQMessageTransport> _logger;
        private readonly IRabbitMQConnectionChannelPool _connectionChannelPool;

        public RabbitMQMessageTransport(IServiceProvider serviceProvider, ILogger<RabbitMQMessageTransport> logger, IRabbitMQConnectionChannelPool connectionChannelPool)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _connectionChannelPool = connectionChannelPool;
        }

        public OperationResponse Send(MessageCarrier message, string exchangeType = "topic")
        {
            IModel channel = null;
            try
            {
                channel = _connectionChannelPool.Rent();
                channel.ConfirmSelect();
                var props = channel.CreateBasicProperties();
                props.DeliveryMode = 2;//发送模式1为不持续，2为持续
                props.Headers = message.MessageHeader.ToDictionary(x => x.Key, x => (object)x.Value);
                channel.ExchangeDeclare(exchange: message.GetExchange(), type: exchangeType, durable: true);
                channel.BasicPublish(message.GetExchange(), message.GetRoutingKey(), props, message.Body);//发布消息到MQ
                channel.WaitForConfirmsOrDie(TimeSpan.FromSeconds(5));
                _logger.LogInformation($"发送消息到RabbitMQ成功,exchange:{message.GetExchange()}----->routingkey:{message.GetRoutingKey()}------->messageid:{message.GetId()}");
                return new OperationResponse(OperationEnumType.Success);
            }
            catch (Exception ex)
            {
                return new OperationResponse($"{ex.Message}{ex.StackTrace}", OperationEnumType.Error);
            }
            finally
            {
                if (channel != null)
                {
                    _connectionChannelPool.Return(channel);//使用完成后还给对象池
                }
            }
        }

        public Task<OperationResponse> SendAsync(MessageCarrier message, string exchangeType = "topic")
        {
            IModel channel = null;
            try
            {
                channel = _connectionChannelPool.Rent();
                channel.ConfirmSelect();
                var props = channel.CreateBasicProperties();
                props.DeliveryMode = 2;//发送模式1为不持续，2为持续
                props.Headers = message.MessageHeader.ToDictionary(x => x.Key, x => (object)x.Value);
                channel.ExchangeDeclare(exchange: message.GetExchange(), type: exchangeType, durable: true);
                channel.BasicPublish(message.GetExchange(), message.GetRoutingKey(), props, message.Body);//发布消息到MQ
                channel.WaitForConfirmsOrDie(TimeSpan.FromSeconds(5));
                _logger.LogInformation($"发送消息到RabbitMQ成功,exchange:{message.GetExchange()}----->routingkey:{message.GetRoutingKey()}------->messageid:{message.GetId()}");
                return Task.FromResult(new OperationResponse(OperationEnumType.Success));
            }
            catch (Exception ex)
            {
                return Task.FromResult(new OperationResponse($"{ex.Message}{ex.StackTrace}", OperationEnumType.Error));
            }
            finally
            {
                if(channel!=null)
                {
                    _connectionChannelPool.Return(channel);//使用完成后还给对象池
                }
            }
        }
    }
}
