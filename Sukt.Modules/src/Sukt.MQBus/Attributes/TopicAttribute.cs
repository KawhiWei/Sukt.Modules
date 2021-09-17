using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.MQBus.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class TopicAttribute : Attribute
    {
        public TopicAttribute(string exChange, string topic, string queue)
        {
            ExChange = exChange;
            Topic = topic;
            Queue = queue;
        }

        /// <summary>
        /// 交换机名称
        /// </summary>
        public string ExChange { get; set; }
        /// <summary>
        /// 交换机类型
        /// </summary>
        public string Type => "";
        /// <summary>
        /// 匹配符
        /// </summary>
        public string Topic { get; set; }
        /// <summary>
        /// 队列名称
        /// </summary>
        public string Queue { get; set; }
    }
}
