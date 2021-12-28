using System;

namespace Sukt.Module.Core.SuktDependencyAppModule
{
    /// <summary>
    /// 服务提供者访问器接口
    /// </summary>
    public interface IServiceProviderAccessor
    {
        IServiceProvider ServiceProvider { get; }
    }
}