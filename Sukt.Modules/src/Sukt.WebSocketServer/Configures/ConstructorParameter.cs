using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Sukt.WebSocketServer.Configures
{
    /// <summary>
    /// 构造函数参数配置
    /// Constructor parameter
    /// </summary>
    public class ConstructorParameter
    {
        /// <summary>
        /// 构造函数信息
        /// </summary>
        public ConstructorInfo ConstructorInfo { get; set; }
        /// <summary>
        /// 构造函数参数
        /// Parameter in constructor
        /// </summary>
        public ParameterInfo[] ParameterInfos { get; set; }
    }
}
