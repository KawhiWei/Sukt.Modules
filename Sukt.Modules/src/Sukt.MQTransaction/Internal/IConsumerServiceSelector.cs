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
        IEnumerable<ConsumerExecutorDescriptor> SelectConsumersFromInterfaceTypes();
    }
}
