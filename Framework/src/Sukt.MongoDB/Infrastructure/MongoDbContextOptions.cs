namespace Sukt.MongoDB.Infrastructure
{
    public class MongoDbContextOptions : IMongoDbContextOptions
    {
        public string ConnectionString { get; set; }
    }
}