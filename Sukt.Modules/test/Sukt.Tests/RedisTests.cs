using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Sukt.Module.Core.Modules;
using Sukt.Module.Core.SuktDependencyAppModule;
using Sukt.Redis;
using Sukt.TestBase;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Sukt.Tests
{
    public class RedisTests : IntegratedTest<RedisModule>
    {
        private readonly IRedisRepository _redisRepository;

        public RedisTests()
        {
            _redisRepository = ServiceProvider.GetService<IRedisRepository>();
        }

        [Fact]
        public async Task String_Test()
        {
            var source = "adsadadasda";
            await _redisRepository.SetAsync("test", source, TimeSpan.FromMinutes(20));
            var target = await _redisRepository.GetStringAsync("test");
            target.ShouldBe(source);
            await _redisRepository.RemoveAsync("test");
        }
        [Fact]
        public async Task ListLeftPush_Test()
        {
            var source = "123456";
            await _redisRepository.SetListLeftPushAsync("listleft_test", source);
            var target = await _redisRepository.GetListLeftPopAsync("listleft_test");
            target.ShouldBe(source);
        }
        [Fact]
        public async Task ListRightPush_Test()
        {
            var source = "123456";
            await _redisRepository.SetListLeftPushAsync("listleft_test", source);
            var target = await _redisRepository.GetListLeftPopAsync("listleft_test");
            target.ShouldBe(source);
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
