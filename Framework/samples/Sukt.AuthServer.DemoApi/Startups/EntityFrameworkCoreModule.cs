using Microsoft.Extensions.DependencyInjection;
using Sukt.EntityFrameworkCore;
using Sukt.Module.Core.AppOption;
using Sukt.Module.Core.Extensions;
using System.Linq;

namespace Sukt.AuthServer.DemoApi.Startups
{
    public class EntityFrameworkCoreModule : EntityFrameworkCoreBaseModule
    {
        public override void AddDbContextWithUnitOfWork(IServiceCollection services)
        {
            var settings = services.GetAppSettings();
            var configuration = services.GetConfiguration();
            services.Configure<AppOptionSettings>(configuration.GetSection("SuktCore"));
            services.AddSuktDbContext<SuktContext>(x =>
            {
                x.ConnectionString = settings.DbContexts.Values.First().ConnectionString;
                x.DatabaseType = settings.DbContexts.Values.First().DatabaseType;
                x.MigrationsAssemblyName = settings.DbContexts.Values.First().MigrationsAssemblyName;
            });
            services.AddUnitOfWork<SuktContext>();
        }
    }
}
