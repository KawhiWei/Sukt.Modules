using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Sukt.MQTransaction
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
        public SubscribeAttribute SuktSubscribeAttribute {get;set;}
        /// <summary>
        /// 服务实现类型
        /// </summary>
        public TypeInfo ImplementationTypeInfo { get; set; }
        /// <summary>
        /// 服务类型
        /// </summary>
        public TypeInfo ServiceTypeInfo { get; set; }
        /// <summary>
        /// 方法参数
        /// </summary>
        public IList<ParameterDescriptor> ParameterDescriptors { get; set; }
    }
    public class ParameterDescriptor
    {
        public string Name { get; set; }

        public Type ParameterType { get; set; }
    }
}
