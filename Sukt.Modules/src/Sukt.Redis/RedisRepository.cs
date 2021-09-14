using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using Sukt.Module.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.Redis
{
    public class RedisRepository : IRedisRepository
    {
        private readonly ILogger<RedisRepository> _logger;
        private readonly ConnectionMultiplexer _connectionMultiplexer;
        private readonly IDatabase _database;

        public RedisRepository(ILogger<RedisRepository> logger, ConnectionMultiplexer connectionMultiplexer)
        {
            _logger = logger;
            _connectionMultiplexer = connectionMultiplexer;
            _database = connectionMultiplexer.GetDatabase();
        }

        public async Task<bool> ExistAsync(string key)
        {
            return await _database.KeyExistsAsync(key);
        }
        public async Task<string> GetStringAsync(string key)
        {
            return await _database.StringGetAsync(key);
        }
        public async Task RemoveAsync(string key)
        {
            await _database.KeyDeleteAsync(key);
        }
        public async Task SetJsonAsync(string key,object value,TimeSpan expiretime)
        {
            if(value!=null)
            {
                await _database.StringSetAsync(key, value.ToJson(), expiretime);
            }
        }
        public async Task SetStringAsync(string key, string value, TimeSpan expiretime)
        {
            if (value != null)
            {
                await _database.StringSetAsync(key, value, expiretime);
            }
        }
        public async Task<TEntity> GetAsync<TEntity>(string key)
        {
            var value= await _database.StringGetAsync(key);
            if (value.HasValue)
            {
               return value.ToString().FromJson<TEntity>();
            }
            return default;
        }
        public async Task<RedisValue[]> GetListRangeAsync(string key)
        {
            return await _database.ListRangeAsync(key);
        }
        /// <summary>
        /// 在list头部插入值，不存在创建
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<long> SetListLeftPushAsync(string key,string value)
        {
            return await _database.ListLeftPushAsync(key, value);
        }
        /// <summary>
        /// 在list尾部插入值，不存在创建
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<long> SetListRightPushAsync(string key, string value)
        {
            return await _database.ListRightPushAsync(key, value);
        }
        /// <summary>
        /// 返回该键的第一个值并删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task <T> GetListLeftPopAsync<T>(string key)
        {
            var cachevalue = await _database.ListLeftPopAsync(key);
            if (cachevalue.ToString().IsNullOrEmpty())
            {
                return default;
            }
            return cachevalue.ToString().FromJson<T>();
        }
        /// <summary>
        /// 返回该键的最后一个值并删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<T> GetListRightPopAsync<T>(string key)
        {
            var cachevalue = await _database.ListRightPopAsync(key);
            if (cachevalue.ToString().IsNullOrEmpty())
            {
                return default;
            }
            return cachevalue.ToString().FromJson<T>();
        }
        /// <summary>
        /// 返回该键的第一个值并删除
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<string> GetListLeftPopAsync(string key)
        {
            return await _database.ListLeftPopAsync(key);
        }
        /// <summary>
        /// 返回该键的最后一个值并删除
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<string> GetListRightPopAsync(string key)
        {
            return await _database.ListRightPopAsync(key);
            //_database.SortedSetAddAsync
        }
        public async Task<long> GetListLengthAsync(string key)
        {
            return await _database.ListLengthAsync(key);
        }
        /// <summary>
        /// 返回在该键上列表对应的元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public async Task<IEnumerable<string>> GetListRangeAsync(string redisKey, int db = -1)
        {
            var result = await _database.ListRangeAsync(redisKey);
            return result.Select(o => o.ToString());
        }
        /// <summary>
        /// 根据索引获取数据
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        public async Task<IEnumerable<string>> GetListRangeAsync(string redisKey, int start, int stop)
        {
            var result = await _database.ListRangeAsync(redisKey, start, stop);
            return result.Select(o => o.ToString());
        }
        /// <summary>
        /// 删除List中的元素 并返回删除的个数
        /// </summary>
        /// <param name="redisKey">key</param>
        /// <param name="redisValue">元素</param>
        /// <param name="type">大于零 : 从表头开始向表尾搜索，小于零 : 从表尾开始向表头搜索，等于零：移除表中所有与 VALUE 相等的值</param>
        /// <returns></returns>
        public async Task<long> RemoveListRangeAsync(string redisKey, string redisValue, long type = 0)
        {
            return await _database.ListRemoveAsync(redisKey, redisValue, type);
        }
        /// <summary>
        /// 清空List
        /// </summary>
        /// <param name="redisKey"></param>
        public async Task ClearListAsync(string redisKey)
        {
            await _database.ListTrimAsync(redisKey, 1, 0);
        }
        #region 分布式锁
        /// <summary>
        /// 分布式锁 Token。
        /// </summary>
        private static readonly RedisValue LockToken = Environment.MachineName;

        /// <summary>
        /// 获取锁
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiretime"></param>
        /// <returns></returns>
        public  bool Lock(string key, TimeSpan expiretime)
        {
            return _database.LockTake(key, LockToken, expiretime);
        }
        /// <summary>
        /// 释放锁
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool UnLock(string key)
        {
            return _database.LockRelease(key, LockToken);
        }
        /// <summary>
        /// 获取锁
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiretime"></param>
        /// <returns></returns>
        public async Task<bool> LockAsync(string key, TimeSpan expiretime)
        {
            return await _database.LockTakeAsync(key, LockToken, expiretime);
        }
        /// <summary>
        /// 释放锁
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<bool> UnLockAsync(string key)
        {
            return await _database.LockReleaseAsync(key, LockToken);
        }
        #endregion
    }
}
