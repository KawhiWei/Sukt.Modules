﻿using Microsoft.EntityFrameworkCore;
using Sukt.Module.Core.AppOption;
using Sukt.Module.Core.Domian;

namespace Sukt.EntityFrameworkCore.DbDrivens
{
    /// <summary>
    /// SqlServer驱动提供者
    /// </summary>
    public class SqlServerDbContextDrivenProvider : IDbContextDrivenProvider
    {
        public DBType DatabaseType => DBType.SqlServer;

        public DbContextOptionsBuilder Builder(DbContextOptionsBuilder builder, string connectionString, DestinyContextOptionsBuilder optionsBuilder)
        {
            builder.UseSqlServer(connectionString, opt => opt.MigrationsAssembly(optionsBuilder.MigrationsAssemblyName)).EnableSensitiveDataLogging().UseSnakeCaseNamingConvention();
            return builder;
        }
    }
}
