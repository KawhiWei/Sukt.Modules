using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Sukt.MQTransaction
{
    /// <summary>
    /// 根据《ISuktMQCapSubscribe》获取用户处理方法或者类
    /// </summary>
    public interface IConsumerServiceSelector
    {
        ConcurrentDictionary<string, IReadOnlyList<ConsumerExecutorDescriptor>> GetSubscribe();
        /// <summary>
        /// 获取所有订阅者方法
        /// </summary>
        /// <returns></returns>
        ConcurrentBag<ConsumerExecutorDescriptor> SelectConsumersFromInterfaceTypes();
        /// <summary>
        /// 获取一个实例
        /// </summary>
        /// <param name="exchange"></param>
        /// <param name="routingkey"></param>
        /// <param name="descriptor"></param>
        /// <returns></returns>
        bool TryGetConsumerExecutorDescriptorByRoutingkey(string exchange, string routingkey, out ConsumerExecutorDescriptor descriptor);
    }
}
