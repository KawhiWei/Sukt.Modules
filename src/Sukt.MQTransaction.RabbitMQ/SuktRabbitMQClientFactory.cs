using Microsoft.Extensions.Options;
using Sukt.Module.Core.Exceptions;
using Sukt.MQTransaction.Factory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.MQTransaction.RabbitMQ
{
    internal sealed class SuktRabbitMQClientFactory : ISuktMQClientFactory
    {
        private readonly IRabbitMQConnectionChannelPool _rabbitMQConnectionChannelPool;
        private readonly IOptions<RabbiMQOptions> _options;

        public SuktRabbitMQClientFactory(IRabbitMQConnectionChannelPool rabbitMQConnectionChannelPool, IOptions<RabbiMQOptions> options)
        {
            _rabbitMQConnectionChannelPool = rabbitMQConnectionChannelPool;
            _options = options;
        }

        public ISuktSubscribeClient Create(string exchange, string topicOrRoutingKeyName, string queue)
        {
            try
            {
                var client = new SuktRabbitMQSubscribeClient(_options, _rabbitMQConnectionChannelPool);
                return client;
            }
            catch (SuktAppException ex)
            {
                throw new SuktAppException($"{ex.Message}---------->",ex);
            }
        }
    }
}
