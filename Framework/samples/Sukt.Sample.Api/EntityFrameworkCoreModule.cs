using Sukt.EntityFrameworkCore;
using Sukt.Module.Core.AppOption;
using Sukt.Module.Core.Extensions;

namespace Sukt.Sample.Api
{
    public class EntityFrameworkCoreModule : EntityFrameworkCoreBaseModule
    {
        public override void AddDbContextWithUnitOfWork(IServiceCollection services)
        {
            var settings = services.GetAppSettings();
            var configuration = services.GetConfiguration();
            services.Configure<AppOptionSettings>(configuration.GetSection("SuktCore"));
            services.AddSuktDbContext<SampleDbContext>(x =>
            {
                x.DatabaseType = DBType.InMemory;
                x.MigrationsAssemblyName = typeof(SampleDbContext).Assembly.GetName().Name;
            });
            services.AddUnitOfWork<SampleDbContext>();
        }
    }
}
