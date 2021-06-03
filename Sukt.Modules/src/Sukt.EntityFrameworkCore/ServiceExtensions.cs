using Microsoft.EntityFrameworkCore;
using Sukt.Module.Core.AppOption;
using Sukt.Module.Core.DbContextDriven;
using Sukt.Module.Core.Entity;
using Sukt.Module.Core.Extensions;
using System;
using System.IO;
using System.Linq;
using Sukt.EntityFrameworkCore;
using Sukt.Module.Core.Exceptions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceExtensions
    {
        /// <summary>
        /// 添加上下文，自动识别数据库驱动
        /// </summary>
        /// <typeparam name="TDbContext">上下文</typeparam>
        /// <param name="services">服务集合</param>
        /// <param name="optionsAction">操作委托</param>
        /// <returns></returns>

        public static IServiceCollection AddSuktDbContext<TDbContext>(this IServiceCollection services, Action<SuktContextOptions> dboption,  Action<IServiceProvider, DbContextOptionsBuilder> optionsAction = null) where TDbContext : SuktDbContextBase
        {
            if(dboption==null)
            {
                throw new SuktAppException(nameof(dboption));
            }
            SuktContextOptions options = new SuktContextOptions();
            dboption(options);
            var stest = services.GetAppSettings();
            var type1 = typeof(TDbContext);
            //SuktContextOptions contextOptions = stest.DbContexts?.Values.FirstOrDefault(o => o.DbContextType == type1);
            services.AddHealthChecks().AddDbContextCheck<TDbContext>();
            services.AddDbContext<TDbContext>((provider, builder) =>
            {
                var type = typeof(TDbContext);
                var settings = provider.GetAppSettings();
                if (settings == null)
                {
                    MessageBox.Show("配置不存在!!");
                }
                //SuktContextOptions contextOptions = settings.DbContexts?.Values.FirstOrDefault(o => o.DbContextType == type);
                if (options is null)
                {
                    MessageBox.Show($"无法找到{type.Name}数据库配置信息!!");
                }
                var databaseType = options.DatabaseType;
                //if (databaseType == Destiny.Core.Flow.Entity.DatabaseType.SqlServer)              
                //每个类型都要判断。可以使用一个接口，每种类型实现自己的，根据数据类型得到相关驱动，使用（策略模式？工厂模式？？）
                //配合注入完美
                //{ 
                //}
                var drivenProvider = provider.GetServices<IDbContextDrivenProvider>().FirstOrDefault(o => o.DatabaseType == databaseType);
                if (drivenProvider == null)
                {
                    MessageBox.Show($"没有找到{databaseType}类型的驱动");

                }
                DestinyContextOptionsBuilder optionsBuilder1 = new DestinyContextOptionsBuilder();
                optionsBuilder1.MigrationsAssemblyName = options.MigrationsAssemblyName;
                var connectionString = options.ConnectionString;
                if (Path.GetExtension(options.ConnectionString).ToLower() == ".txt") //txt文件
                {
                    connectionString = provider.GetFileText(options.ConnectionString, $"未找到存放{databaseType.ToDescription()}数据库链接的文件");
                }
                builder = drivenProvider.Builder(builder, connectionString, optionsBuilder1);
                optionsAction?.Invoke(provider, builder);
            });
            return services;

        }
    }
}
