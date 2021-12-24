using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.MQTransaction.Factory
{
    /// <summary>
    /// 消息队列工厂
    /// </summary>
    public interface ISuktMQClientFactory
    {
        ISuktSubscribeClient Create(string exchange, string topicOrRoutingKeyName, string queue);
    }
}
