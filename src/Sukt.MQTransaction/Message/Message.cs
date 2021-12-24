using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.MQTransaction
{
    public class Message
    {
        public Message() { }

        public Message(IDictionary<string, string> messageHeader, object messageContent)
        {
            MessageHeader = messageHeader ?? throw new ArgumentNullException(nameof(messageHeader));
            MessageContent = messageContent;
        }

        /// <summary>
        /// 消息头
        /// </summary>
        public IDictionary<string, string> MessageHeader { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public object MessageContent { get; set; }
    }
    public static class MessageExtensions
    {
        public static Guid GetId(this Message message)
        {
            message.MessageHeader.TryGetValue(MQTransactionHeaderkeyConst.MessageId, out string value);
            Guid.TryParse(value, out Guid id);
            return id;
        }
        public static string GetExchange(this Message message)
        {
            message.MessageHeader.TryGetValue(MQTransactionHeaderkeyConst.MessageExchange, out string value);
            return value;
        }
        public static string GetRoutingKey(this Message message)
        {
            message.MessageHeader.TryGetValue(MQTransactionHeaderkeyConst.MessageRoutingkey, out string value);
            return value;
        }
    }
}
