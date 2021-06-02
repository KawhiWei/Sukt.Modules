using Microsoft.Extensions.DependencyInjection;
using Sukt.Module.Core.Modules;

namespace Sukt.MultiTenancy
{
    public abstract class MultiTenancyModuleBase : SuktAppModule
    {
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.Services.AddScoped<TenantInfo>();
        }
    }
}