using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Sukt.MQCAP;
using Sukt.MQCAP.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSuktMQCap(this IServiceCollection services)
        {
            services.AddSingleton<IServiceCollection>();
            services.TryAddSingleton<IConsumerServiceSelector, ConsumerServiceSelector>();
            services.TryAddSingleton<IConsumerRegister, ConsumerRegister>();
            services.TryAddEnumerable(ServiceDescriptor.Singleton<IProcessingServer,IConsumerRegister>(serviceProvider=>serviceProvider.GetRequiredService<IConsumerRegister>()));
            services.AddSingleton<BackgroundSubscribe>();
            services.AddHostedService(serviceProvider => serviceProvider.GetRequiredService<BackgroundSubscribe>());
            services.AddSingleton<IBackgroundSubscribe>(serviceProvider => serviceProvider.GetRequiredService<BackgroundSubscribe>());
            return services;
        }
    }
}
