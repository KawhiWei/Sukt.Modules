using Sukt.MQTransaction.Storage;
using System;
using System.Threading.Tasks;

namespace Sukt.MQTransaction.RedisStroe
{
    public class RedisSuktMQTransactionStorage : ISuktMQTransactionStorage
    {
        
        public async Task ChangePublishStateAsync(DbMessage message)
        {
            await Task.CompletedTask;
            throw new NotImplementedException();
        }
        public async Task AddMessageToStorage(DbMessage message)
        {


            await Task.CompletedTask;
        }
    }
}
