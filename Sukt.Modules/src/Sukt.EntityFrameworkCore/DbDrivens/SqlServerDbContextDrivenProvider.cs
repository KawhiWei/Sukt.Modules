using Microsoft.EntityFrameworkCore;
using Sukt.Module.Core.AppOption;
using Sukt.Module.Core.DbContextDriven;
using Sukt.Module.Core.Entity;

namespace Sukt.EntityFrameworkCore.DbDrivens
{
    /// <summary>
    /// SqlServer驱动提供者
    /// </summary>
    public class SqlServerDbContextDrivenProvider : IDbContextDrivenProvider
    {
        public DatabaseType DatabaseType => DatabaseType.SqlServer;

        public DbContextOptionsBuilder Builder(DbContextOptionsBuilder builder, string connectionString, DestinyContextOptionsBuilder optionsBuilder)
        {
            builder.UseSqlServer(connectionString, opt => opt.MigrationsAssembly(optionsBuilder.MigrationsAssemblyName));
            return builder;
        }
    }
}
