using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Sukt.Module.Core.Modules;
using Sukt.Module.Core.SuktDependencyAppModule;

namespace Sukt.Module.Core.Modules
{
    public static class ApplicationInitializationExtensions
    {
        public static IApplicationBuilder GetApplicationBuilder(this ApplicationContext applicationContext)
        {
            return applicationContext.ServiceProvider.GetRequiredService<IObjectAccessor<IApplicationBuilder>>().Value;
        }
    }
}