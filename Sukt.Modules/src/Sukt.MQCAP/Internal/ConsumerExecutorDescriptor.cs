using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Sukt.MQCAP
{
    /// <summary>
    /// 用户定义方法的描述符。
    /// </summary>
    public class ConsumerExecutorDescriptor
    {
        /// <summary>
        /// 消费者处理方法
        /// </summary>
        public MethodInfo MethodInfo { get; set; }
        /// <summary>
        /// 消费者方法特性
        /// </summary>
        public ISuktSubscribeAttribute SuktSubscribeAttribute {get;set;}
        /// <summary>
        /// 消费者类元数据
        /// </summary>
        public TypeInfo ClassTypeInfo { get; set; }
    }
}
