using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sukt.EntityFrameworkCore;
using Sukt.Module.Core.AppOption;
using Sukt.TestBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sukt.Tests
{
    public class EntityFrameworkCoreTest : IntegratedTest<EntityFrameworkCoreModule>
    {
        [Fact]
        public void Test()
        {
            
        }
    }

    public class EntityFrameworkCoreModule : EntityFrameworkCoreBaseModule
    {
        public override void AddDbContextWithUnitOfWork(IServiceCollection services)
        {
            services.AddSuktDbContext<SuktContext>(x =>
            {
                x.ConnectionString = "User ID=postgres;Password=P@ssW0rd;Host=192.168.31.175;Port=15432;Database=sukt.job";
                x.DatabaseType = DBType.MySql;
                x.MigrationsAssemblyName = "adasda";
            });
            services.AddUnitOfWork<SuktContext>();
        }
    }
    public class SuktContext : SuktDbContextBase
    {
        public SuktContext(DbContextOptions<SuktContext> options, IServiceProvider serviceProvider)
          : base(options, serviceProvider)
        {
        }
    }
}
