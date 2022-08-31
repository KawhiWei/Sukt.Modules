﻿using Microsoft.EntityFrameworkCore;
using Sukt.Module.Core.AppOption;
using Sukt.Module.Core.Domian;
using System;

namespace Sukt.EntityFrameworkCore.DbDrivens
{
    /// <summary>
    /// MySql驱动提供者
    /// </summary>
    public class MySqlDbContextDrivenProvider : IDbContextDrivenProvider
    {
        public DBType DatabaseType => DBType.InMemory;
        public DbContextOptionsBuilder Builder(DbContextOptionsBuilder builder, string connectionString, DestinyContextOptionsBuilder optionsBuilder)
        {
            builder.UseInMemoryDatabase("In-Memory").EnableSensitiveDataLogging();
            return builder;
        }
    }
}
