﻿using Microsoft.EntityFrameworkCore;
using Sukt.Module.Core.AppOption;
using Sukt.Module.Core.Domian;

namespace Sukt.EntityFrameworkCore.DbDrivens
{
    public class NpgSqlDbContextDrivenProvider : IDbContextDrivenProvider
    {
        public DBType DatabaseType => DBType.PostgreSQL;

        public DbContextOptionsBuilder Builder(DbContextOptionsBuilder builder, string connectionString, DestinyContextOptionsBuilder optionsBuilder)
        {
            builder.UseNpgsql(connectionString, opt => opt.MigrationsAssembly(optionsBuilder.MigrationsAssemblyName)).EnableSensitiveDataLogging().UseSnakeCaseNamingConvention(); ;
            return builder;
        }
    }
}
