using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.MQTransaction
{
    /// <summary>
    /// 消费者运输消息的载体
    /// </summary>
    public class MessageCarrier
    {
        public MessageCarrier()
        {
        }

        public MessageCarrier(IDictionary<string,string> messageHeader,byte[] body)
        {
            MessageHeader = messageHeader;
            Body = body;
        }
        /// <summary>
        /// 消息头
        /// </summary>
        public IDictionary<string, string> MessageHeader { get; set; }
        /// <summary>
        /// 消息二进制内容
        /// </summary>
        [CanBeNull]
        public byte[] Body { get; }
    }
    public static class MessageCarrierExtensions
    {
        public static Guid GetId(this MessageCarrier message)
        {
            message.MessageHeader.TryGetValue(MQTransactionHeaderkeyConst.MessageId, out string value);
            Guid.TryParse(value, out Guid id);
            return id;
        }
        public static string GetExchange(this MessageCarrier message)
        {
            message.MessageHeader.TryGetValue(MQTransactionHeaderkeyConst.MessageExchange, out string value);
            return value;
        }
        public static string GetRoutingKey(this MessageCarrier message)
        {
            message.MessageHeader.TryGetValue(MQTransactionHeaderkeyConst.MessageRoutingkey, out string value);
            return value;
        }
    }
}
