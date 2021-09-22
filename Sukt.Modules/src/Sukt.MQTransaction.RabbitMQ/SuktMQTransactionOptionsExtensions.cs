using Sukt.MQTransaction;
using Sukt.MQTransaction.RabbitMQ;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SuktMQTransactionOptionsExtensions
    {
        public static SuktMQTransactionOptions UseRabbitMQ(this SuktMQTransactionOptions options,Action<RabbiMQOptions> action)
        {
            if(action==null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            options.RegisterExtension(new SuktMQTransactionOptionsExtension(action));
            return options;
        }
    }
}
