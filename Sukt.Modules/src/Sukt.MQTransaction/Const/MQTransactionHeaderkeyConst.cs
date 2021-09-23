using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.MQTransaction
{
    public class MQTransactionHeaderkeyConst
    {
        /// <summary>
        /// 消息存储表Id
        /// </summary>
        public const string MessageId = "mqtransaction-msg-id";
        /// <summary>
        /// 消息投递的RoutingKey
        /// </summary>
        public const string MessageRoutingkey = "mqtransaction-msg-routingkey";
        /// <summary>
        /// 消息投递的交换机
        /// </summary>
        public const string MessageExchange = "mqtransaction-msg-exchange";
        /// <summary>
        /// 消息发送时间
        /// </summary>
        public const string MessageSendTime = "mqtransaction-msg-sendtime";
        /// <summary>
        /// 消息反射类型
        /// </summary>
        public const string MessageType = "mqtransaction-msg-type";
        /// <summary>
        /// 消息的回调名称
        /// </summary>
        public const string MessageCallbackName = "mqtransaction-msg-callbackname";
    }
}
