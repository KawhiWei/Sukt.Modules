using Microsoft.Extensions.DependencyInjection;

namespace Sukt.Module.Core.Extensions
{
    public static class EventBusExtensions
    {
        public static IServiceCollection AddEvents(this IServiceCollection services)
        {
            // services.TryAddTransient<IEventBus, InMemoryDefaultBus>();//事件总线需要使用瞬时注入，否则在过滤器内无法获取当前字典
            return services;
        }
    }
}
