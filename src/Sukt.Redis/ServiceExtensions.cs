using StackExchange.Redis;
using Sukt.Module.Core.Exceptions;
using Sukt.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceExtensions
    {
        public static void AddRedis(this IServiceCollection services, string connectionString)
        {
            if (services == null)
                throw new SuktAppException(nameof(services));
            // 配置启动Redis服务，虽然可能影响项目启动速度，但是不能在运行的时候报错，所以是合理的
            services.AddSingleton<ConnectionMultiplexer>(sp =>
            {
                var configuration = ConfigurationOptions.Parse(connectionString, true);
                configuration.ResolveDns = true;
                return ConnectionMultiplexer.Connect(configuration);
            });
        }
        public static void AddDefaultRedisRepository(this IServiceCollection services)
        {
            services.AddTransient<IRedisRepository, RedisRepository>();
        }
    }
}
