using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.MQTransaction
{
    public interface ISuktMQTransactionExtension
    {
        void AddService(IServiceCollection services);
    }
}
