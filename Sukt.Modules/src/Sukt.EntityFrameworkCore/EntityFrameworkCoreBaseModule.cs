using Microsoft.Extensions.DependencyInjection;
using Sukt.EntityFrameworkCore.DbDrivens;
using Sukt.Module.Core.DbContextDriven;
using Sukt.Module.Core.Modules;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.EntityFrameworkCore
{
    public abstract class EntityFrameworkCoreBaseModule: SuktAppModule
    {
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            this.AddDbDriven(context.Services);
            context.Services.AddRepository();
            //context.Services.AddUnitOfWork
            //AddDbContextWithUnitOfWork(context.Services);
            //AddRepository(context.Services);
        }
        /// <summary>
        /// 添加DB驱动
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        protected virtual IServiceCollection AddDbDriven(IServiceCollection services)
        {

            services.AddSingleton<IDbContextDrivenProvider, MySqlDbContextDrivenProvider>();
            services.AddSingleton<IDbContextDrivenProvider, SqlServerDbContextDrivenProvider>();
            return services;
        }
    }
}
