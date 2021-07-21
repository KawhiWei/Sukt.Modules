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
            await _redisRepository.SetAsync("test", source, TimeSpan.FromMinutes(20));
            var target = await _redisRepository.GetStringAsync("test");
            target.ShouldBe(source);
            await _redisRepository.RemoveAsync("test");
        }
        /// <summary>
        /// 在List头部插入值
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task ListLeftPush_Test()
        {
            var source = "123456";
            await _redisRepository.SetListLeftPushAsync("listleft_test", source);
            var target = await _redisRepository.GetListLeftPopAsync("listleft_test");
            target.ShouldBe(source);
        }
        /// <summary>
        /// 在List尾部插入值
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task ListRightPush_Test()
        {
            var source = "123456";
            await _redisRepository.SetListLeftPushAsync("listleft_test", source);
            var target = await _redisRepository.GetListLeftPopAsync("listleft_test");
            target.ShouldBe(source);
        }
        /// <summary>
        /// 分布式锁单元测试
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task DistributedLocker_Test()
        {
            var key = "Order002";
            var lockerkey = await _redisRepository.LockAsync(key, TimeSpan.FromSeconds(180));
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
            service.AddRedis("192.168.0.166:6379,password = redis123,defaultDatabase=5,prefix = test_");
        }
    }
}
