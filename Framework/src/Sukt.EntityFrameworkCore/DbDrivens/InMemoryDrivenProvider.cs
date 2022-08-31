using Microsoft.EntityFrameworkCore;
using Sukt.Module.Core.AppOption;
using Sukt.Module.Core.Domian;
using System;

namespace Sukt.EntityFrameworkCore.DbDrivens
{
    internal class InMemoryDrivenProvider : IDbContextDrivenProvider
    {
        public DBType DatabaseType => throw new NotImplementedException();

        public DbContextOptionsBuilder Builder(DbContextOptionsBuilder builder, string connectionString, DestinyContextOptionsBuilder optionsBuilder)
        {
            throw new NotImplementedException();
        }
    }
}
