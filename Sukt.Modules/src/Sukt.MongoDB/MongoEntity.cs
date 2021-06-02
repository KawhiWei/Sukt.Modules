using MongoDB.Bson;
using Sukt.Module.Core.Entity;

namespace Sukt.MongoDB
{
    public abstract class MongoEntity : IEntity<ObjectId>
    {
        public ObjectId Id { get; set; } = ObjectId.GenerateNewId();
    }
}