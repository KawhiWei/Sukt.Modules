using Microsoft.Extensions.DependencyInjection;
using Sukt.Module.Core.Modules;
using System;

namespace Sukt.Redis
{
    public abstract class RedisModuleBase: SuktAppModule
    {
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.Services.AddDefaultRedisRepository();
            this.AddRedis(context.Services);
        }
        public abstract void AddRedis(IServiceCollection service);
    }
}
