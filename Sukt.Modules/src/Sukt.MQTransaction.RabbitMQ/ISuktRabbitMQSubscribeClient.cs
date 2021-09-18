using Sukt.MQTransaction.Factory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Sukt.MQTransaction.RabbitMQ
{
    internal sealed class ISuktRabbitMQSubscribeClient: ISuktSubscribeClient
    {
        /// <summary>
        /// 添加一部锁，每次只允许一个线程进入
        /// </summary>
        private readonly SemaphoreSlim _semaphoreSlimLock=new SemaphoreSlim(initialCount:1,maxCount:1);


    }
}
