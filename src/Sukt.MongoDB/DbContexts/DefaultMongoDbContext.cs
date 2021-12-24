using Sukt.MongoDB.Infrastructure;
using System.Diagnostics.CodeAnalysis;

namespace Sukt.MongoDB.DbContexts
{
    public class DefaultMongoDbContext : MongoDbContextBase
    {
        public DefaultMongoDbContext([NotNull] MongoDbContextOptions options) : base(options)
        {
        }
    }
}