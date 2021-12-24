using Microsoft.Extensions.DependencyInjection;

namespace Sukt.Module.Core.Modules
{
    public class ModulesOptions
    {
        public IServiceCollection Service { get; }

        public ModulesOptions(IServiceCollection service)
        {
            Service = service;
        }
    }
}