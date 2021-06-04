using System;

namespace Sukt.Module.Core.Modules
{
    /// <summary>
    /// 定义模块加载接口
    /// </summary>
    public interface ISuktAppModule : IApplicationInitialization
    {
        void ConfigureServices(ConfigureServicesContext context);
        /// <summary>
        /// 配置
        /// </summary>
        /// <typeparam name="TOptions"></typeparam>
        /// <param name="configureOptions">配置选项</param>
        void Configure<TOptions>(Action<TOptions> configureOptions) where TOptions : class;
        /// <summary>
        /// 服务依赖集合
        /// </summary>
        /// <param name="moduleType"></param>
        /// <returns></returns>
        Type[] GetDependedTypes(Type moduleType = null);
        bool Enable { get; set; }
    }
}