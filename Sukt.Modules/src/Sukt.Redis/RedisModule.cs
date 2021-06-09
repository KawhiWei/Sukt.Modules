using Sukt.Module.Core.Modules;
using System;

namespace Sukt.Redis
{
    public abstract class RedisModule: SuktAppModule
    {

        public abstract void AddRedis();
    }
}
