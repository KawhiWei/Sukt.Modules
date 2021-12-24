using Microsoft.Extensions.DependencyInjection;
using Sukt.MQTransaction.Factory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.MQTransaction.RabbitMQ
{
    internal sealed class SuktMQTransactionOptionsExtension : ISuktMQTransactionExtension
    {
        private readonly Action<RabbiMQOptions> _action;
        public SuktMQTransactionOptionsExtension(Action<RabbiMQOptions> action)
        {
            _action = action;
        }
        public void AddService(IServiceCollection services)
        {
            services.Configure(_action);
            services.AddSingleton<IMessageTransport, RabbitMQMessageTransport>();
            services.AddSingleton<IRabbitMQConnectionChannelPool, RabbitMQConnectionChannelPool>();
            services.AddSingleton<ISuktMQClientFactory, SuktRabbitMQClientFactory>();
        }
    }
}
