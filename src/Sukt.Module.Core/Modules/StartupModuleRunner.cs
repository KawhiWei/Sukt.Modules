using Microsoft.Extensions.DependencyInjection;
using Sukt.Module.Core.SuktDependencyAppModule;
using System;

namespace Sukt.Module.Core.Modules
{
    public class StartupModuleRunner : ModuleApplicationBase, IStartupModuleRunner
    {
        /// <summary>
        /// 程序启动运行时
        /// </summary>
        /// <param name="startupModuleType"></param>
        /// <param name="services"></param>
        public StartupModuleRunner(Type startupModuleType, IServiceCollection services)
            : base(startupModuleType, services)
        {
            services.AddSingleton<IStartupModuleRunner>(this);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            SuktIocManage.Instance.SetServiceCollection(services);
            var context = new ConfigureServicesContext(services);
            services.AddSingleton(context);
            foreach (var module in Modules)
            {
                //如果是继承了SuktAppModule
                if(module is SuktAppModule appModule)
                {
                    appModule.ConfigureServicesContext = context;
                }
            }
            foreach (var config in Modules)
            {
                services.AddSingleton(config);
                config.ConfigureServices(context);
            }
            foreach (var module in Modules)
            {
                if (module is SuktAppModule appModule)
                {
                    appModule.ConfigureServicesContext = null;
                }
            }
        }

        public void Initialize(IServiceProvider service)
        {
            SuktIocManage.Instance.SetApplicationServiceProvider(service);
            SetServiceProvider(service);
            using var scope = ServiceProvider.CreateScope();
            //using var scope = service.CreateScope();
            var ctx = new ApplicationContext(scope.ServiceProvider);
            foreach (var cfg in Modules)
            {
                cfg.ApplicationInitialization(ctx);
            }
        }

        public override void Dispose()
        {
            base.Dispose();

            if (ServiceProvider is IDisposable disposableServiceProvider)
            {
                disposableServiceProvider.Dispose();
            }
        }
    }
}