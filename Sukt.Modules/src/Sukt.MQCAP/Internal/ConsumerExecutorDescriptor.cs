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
        /// 
        /// </summary>
        public MethodInfo MethodInfo { get; set; }
        /// <summary>
        /// 用户自定义方法特性
        /// </summary>
        public ISuktSubscribeAttribute SuktSubscribeAttribute {get;set;}
        /// <summary>
        /// 
        /// </summary>
        public TypeInfo ImplTypeInfo { get; set; }
        public 
    }
}
