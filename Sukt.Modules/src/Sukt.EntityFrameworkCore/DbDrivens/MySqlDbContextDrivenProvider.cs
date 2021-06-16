﻿using Microsoft.EntityFrameworkCore;
using Sukt.Module.Core.AppOption;
using Sukt.Module.Core.Entity;
using Sukt.Module.Core.DbContextDriven;
using System;

namespace Sukt.EntityFrameworkCore.DbDrivens
{
    /// <summary>
    /// MySql驱动提供者
    /// </summary>
    public class MySqlDbContextDrivenProvider : IDbContextDrivenProvider
    {
        public DBType DatabaseType => DBType.MySql;
        public DbContextOptionsBuilder Builder(DbContextOptionsBuilder builder, string connectionString, DestinyContextOptionsBuilder optionsBuilder)
        {
            builder.UseMySql(connectionString, new MySqlServerVersion(new Version()), opt => opt.MigrationsAssembly(optionsBuilder.MigrationsAssemblyName));
            return builder;
        }
    }
}
