using StackExchange.Redis;
using Sukt.Module.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.Redis
{
    /// <summary>
    /// Redis仓储操作接口
    /// </summary>
    public interface IRedisRepository: ITransientDependency
    {
        /// <summary>
        /// 判断键是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<bool> ExistAsync(string key);
        /// <summary>
        /// 删除一个键
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task RemoveAsync(string key);
        /// <summary>
        /// 获取一个Key的字符串
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<string> GetStringAsync(string key);
        /// <summary>
        /// 写入缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiretime"></param>
        /// <returns></returns>
        Task SetAsync(string key, object value, TimeSpan expiretime);
        /// <summary>
        /// 写入字符串
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiretime"></param>
        /// <returns></returns>
        Task SetAsync(string key, string value, TimeSpan expiretime);
        /// <summary>
        /// 获取泛型缓存
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<TEntity> GetAsync<TEntity>(string key);
        /// <summary>
        /// 获取RedisValue数组
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<RedisValue[]> GetListRangeAsync(string key);
        /// <summary>
        /// 在list头部插入值，不存在创建
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<long> SetListLeftPushAsync(string key, string value);
        /// <summary>
        /// 在list尾部插入值，不存在创建
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<long> SetListRightPushAsync(string key, string value);
        /// <summary>
        /// 返回该键的第一个值并删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<T> GetListLeftPopAsync<T>(string key);
        /// <summary>
        /// 返回该键的最后一个值并删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<T> GetListRightPopAsync<T>(string key);
        /// <summary>
        /// 返回该键的第一个值并删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<string> GetListLeftPopAsync(string key);
        /// <summary>
        /// 返回该键的最后一个值并删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<string> GetListRightPopAsync(string key);
        /// <summary>
        /// 获取列表长度
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<long> GetListLengthAsync(string key);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        Task<IEnumerable<string>> GetListRangeAsync(string redisKey, int db = -1);
        /// <summary>
        /// 根据索引获取数据
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        Task<IEnumerable<string>> GetListRangeAsync(string redisKey, int start, int stop);
        /// <summary>
        /// 删除List中的元素 并返回删除的个数
        /// </summary>
        /// <param name="redisKey">key</param>
        /// <param name="redisValue">元素</param>
        /// <param name="type">大于零 : 从表头开始向表尾搜索，小于零 : 从表尾开始向表头搜索，等于零：移除表中所有与 VALUE 相等的值</param>
        /// <returns></returns>
        Task<long> RemoveListRangeAsync(string redisKey, string redisValue, long type = 0);
        /// <summary>
        /// 清空List
        /// </summary>
        /// <param name="redisKey"></param>
        Task ClearListAsync(string redisKey);
        /// <summary>
        /// 获取锁
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiretime"></param>
        /// <returns></returns>
        bool Lock(string key, TimeSpan expiretime);
        /// <summary>
        /// 释放锁
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool UnLock(string key);
        /// <summary>
        /// 获取锁
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiretime"></param>
        /// <returns></returns>
        Task<bool> LockAsync(string key, TimeSpan expiretime);
        /// <summary>
        /// 释放锁
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<bool> UnLockAsync(string key);
    }
}
