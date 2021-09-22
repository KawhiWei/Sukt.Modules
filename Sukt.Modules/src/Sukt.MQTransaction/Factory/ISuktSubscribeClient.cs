using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Sukt.MQTransaction.Factory
{
    /// <summary>
    /// MQ订阅者提供客户
    /// </summary>
    public interface ISuktSubscribeClient:IDisposable
    {
        /// <summary>
        /// 消費者队里绑定exchang
        /// </summary>
        /// <param name="exchange"></param>
        /// <param name="topicOrRoutingKeyName"></param>
        /// <param name="queue"></param>
        /// <param name="exchangeType"></param>
        void SubscribeQueueBind(string exchange, string topicOrRoutingKeyName, string queue, string exchangeType = "topic");
        /// <summary>
        /// 消费者队列监听
        /// </summary>
        /// <param name="exchange"></param>
        /// <param name="topicOrRoutingKeyName"></param>
        /// <param name="queue"></param>
        /// <param name="timedelay"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="exchangeType"></param>
        void SubscribeListening(string exchange, string topicOrRoutingKeyName, string queue,TimeSpan timedelay, CancellationToken cancellationToken, string exchangeType = "topic");
        /// <summary>
        /// 消息处理载体
        /// </summary>
        event EventHandler<MessageCarrier> OnMessageReceived;
    }
}
