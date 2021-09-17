using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.MQTransaction
{
    /// <summary>
    /// MQ事件特性，只允许在方法上，允许多个
    /// 路由键模式
    /// </summary>
    [AttributeUsage(AttributeTargets.Method,AllowMultiple =true)]
    public class RoutingKeySubscribeAttribute:Attribute, ISuktSubscribeAttribute
    {
        public RoutingKeySubscribeAttribute(string exchange, string routingKey = "", string queue="")
        {
            Exchange = exchange;
            RoutingKey = routingKey;
            Queue = queue;
        }
        /// <summary>
        /// 队列名称
        /// </summary>
        public string Queue { get; private set; }
        /// <summary>
        /// 交换机名称
        /// </summary>
        public string Exchange { get; private set; }
        /// <summary>
        /// 队列类型
        /// </summary>
        public string RoutingKey { get; private set; }
        /// <summary>
        /// 交换机类型
        /// </summary>
        public string Type => "";


    }
}
