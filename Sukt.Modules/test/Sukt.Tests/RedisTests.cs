using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Sukt.Module.Core.Modules;
using Sukt.Module.Core.SuktDependencyAppModule;
using Sukt.Redis;
using Sukt.TestBase;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Sukt.Tests
{
    /// <summary>
    /// Redis 单元测试
    /// </summary>
    public class RedisTests : IntegratedTest<RedisModule>
    {
        private readonly IRedisRepository _redisRepository;

        public RedisTests()
        {
            _redisRepository = ServiceProvider.GetService<IRedisRepository>();
        }
        /// <summary>
        /// 写入字符串
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task String_Test()
        {
            var source = "adsadadasda";
            await _redisRepository.SetStringAsync("test", source, TimeSpan.FromMinutes(20));
            var target = await _redisRepository.GetStringAsync("test");
            target.ShouldBe(source);
            await _redisRepository.RemoveAsync("test");
        }
        /// <summary>
        /// 在List头部循环插入值
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task ListLeftTopPush_Test()
        {
            var value = "Sukt.Core.Top";
            for (int i = 0; i < 80; i++)
            {
                await _redisRepository.SetListLeftPushAsync("listleft_top_test", $"{value }---------------------{i}");
            }
        }
        /// <summary>
        /// List头部插入和取出值
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetListLeftTopPop_Test()
        {
            var value = "Sukt.Core";
            var key = "list_left_top_insert";
            var result = await _redisRepository.SetListLeftPushAsync(key,value);
            var target = await _redisRepository.GetListLeftPopAsync(key);
            target.ShouldBe(value);
        }
        /// <summary>
        /// 在List尾部循环插入值
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task ListRightPush_Test()
        {
            var value = "Sukt.Core.Last";
            for (int i = 0; i < 80; i++)
            {

                await _redisRepository.SetListLeftPushAsync("listleft_last_test", $"{value}+++++++++++++++++{i}");
            }
        }
        /// <summary>
        /// List尾部插入和取出值
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetListRightPush_Test()
        {
            var value = "Sukt.Core";
            var key = "list_left_last_insert";
            var result = await _redisRepository.SetListRightPushAsync(key,value);
            var target = await _redisRepository.GetListRightPopAsync(key);
            target.ShouldBe(value);
        }
        /// <summary>
        /// 分布式锁单元测试
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task DistributedLocker_Test()
        {
            var key = "miaoshakoujiankucun";
            var lockerkey = await _redisRepository.LockAsync(key, TimeSpan.FromSeconds(15));
            try
            {
                if (!lockerkey)
                {
                    //未获取到锁在此处返回异常信息
                }
                else
                {
                    //未获取到锁
                    lockerkey.ShouldBe(true);
                    Console.WriteLine("获取到了锁");
                    //Thread.Sleep(1000);//睡眠一段时间，模拟业务代码
                }
            }
            finally
            {
                //切记要在finally释放锁
                var result = await _redisRepository.UnLockAsync(key);
                result.ShouldBe(true);
            }
        }
    }
    [SuktDependsOn(typeof(DependencyAppModule))]
    public class RedisModule : RedisModuleBase
    {
        public override void AddRedis(IServiceCollection service)
        {
            service.AddRedis("192.168.31.144:6379,password=P@ssW0rd,defaultDatabase=5,prefix=sukt_admin_");
        }
    }
}
