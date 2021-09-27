using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Sukt.MQTransaction;
using Sukt.MQTransaction.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSuktMQTransaction(this IServiceCollection services,Action<SuktMQTransactionOptions> action)
        {
            if(action==null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            services.AddSingleton(_ => services);
            services.TryAddSingleton<IConsumerServiceSelector, ConsumerServiceSelector>();
            services.TryAddSingleton<ISenderMessageToMQ, SenderMessageToMQ>();
            services.TryAddSingleton<IConsumerRegister, ConsumerRegister>();
            services.TryAddSingleton<IDispatcher, Dispatcher>();
            services.TryAddSingleton<ISubscribeInvoker, SubscribeInvoker>();
            services.TryAddSingleton<IMQTransactionPublisher, MQTransactionPublisher>();
            services.TryAddEnumerable(ServiceDescriptor.Singleton<IProcessingServer, IDispatcher>(serviceProvider => serviceProvider.GetRequiredService<IDispatcher>()));
            services.TryAddEnumerable(ServiceDescriptor.Singleton<IProcessingServer,IConsumerRegister>(serviceProvider=>serviceProvider.GetRequiredService<IConsumerRegister>()));
            var options = new SuktMQTransactionOptions();
            action(options);
            foreach (var extension in options.Extensions)
            {
                extension.AddService(services);
            }
            services.Configure(action);
            services.AddSingleton<BackgroundSubscribe>();
            services.AddHostedService(serviceProvider => serviceProvider.GetRequiredService<BackgroundSubscribe>());
            services.AddSingleton<IBackgroundSubscribe>(serviceProvider => serviceProvider.GetRequiredService<BackgroundSubscribe>());
            return services;
        }
    }
}
