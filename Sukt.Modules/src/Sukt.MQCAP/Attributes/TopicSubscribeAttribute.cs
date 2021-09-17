using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.MQCAP
{
    /// <summary>
    /// 通配符模式
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class TopicSubscribeAttribute : Attribute
    {
        public TopicSubscribeAttribute(string exchange, string topicOrRoutingKeyName="", string queue="")
        {
            Exchange = exchange;
            TopicOrRoutingKeyName = topicOrRoutingKeyName;
            Queue = queue;
        }

        /// <summary>
        /// 交换机名称
        /// </summary>
        public string Exchange { get; private set; }
        /// <summary>
        /// 主题或交换机路由名称
        /// </summary>
        public string TopicOrRoutingKeyName { get; private set; }
        /// <summary>
        /// 队列名称
        /// </summary>
        public string Queue { get; private set; }
        /// <summary>
        /// 交换机类型
        /// </summary>
        public string Type => "topic";
    }
}
