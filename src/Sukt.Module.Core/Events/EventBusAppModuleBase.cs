using MediatR;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Sukt.Module.Core.Events.EventBus;
using Sukt.Module.Core.Extensions;
using Sukt.Module.Core.Modules;
using Sukt.Module.Core.SuktReflection;

namespace Sukt.Module.Core.Events
{
    public class EventBusAppModuleBase : SuktAppModule
    {
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            var service = context.Services;
            var assemblys = service.GetOrAddSingletonService<IAssemblyFinder, AssemblyFinder>()?.FindAll();
            service.AddMediatR(assemblys);
            service.TryAddTransient<IMediatorHandler, InMemoryDefaultBus>();//事件总线需要使用瞬时注入，否则在过滤器内无法获取当前字典
        }
    }
}
