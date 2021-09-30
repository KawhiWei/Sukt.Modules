using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.MQTransaction
{
    public interface IDispatcher: IProcessingServer
    {
        /// <summary>
        /// 发布消息处理程序
        /// </summary>
        /// <param name="message"></param>
        void PublishToChannel(DbMessage message);
        /// <summary>
        /// 消费消息处理程序
        /// </summary>
        /// <param name="message"></param>
        /// <param name="consumer"></param>
        void SubscribeToChannel(DbMessage message,ConsumerExecutorDescriptor consumer);
        /// <summary>
        /// 不需要持久化直接发布到MQ
        /// </summary>
        /// <param name="message"></param>
        Task PublishToMQAsync(MessageCarrier message, int isrent);
    }
}
