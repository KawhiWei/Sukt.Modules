using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sukt.WebScoket.Configures
{
    /// <summary>
    /// 集群配置
    /// </summary>
    public class ClusterOption
    {
        /// <summary>
        /// 集群通道名称
        /// Cluster channel name
        /// </summary>
        public string ChannelName { get; set; } = "/cluster";
        /// <summary>
        /// Node链接加入
        /// Node connection link
        /// </summary>
        public string[] Nodes { get; set; }
        /// <summary>
        /// Node类型
        /// </summary>
        public ServiceLevel NodeLevel { get; set; }
        /// <summary>
        /// Instruct the current node enable services. 指导当前节点启用服务。
        /// If set false current node will forward request to other node. 如果设置为false，当前节点将转发请求到其他节点。
        /// Only NodeLevel are valid for Master. 只有NodeLevel对Master有效。
        /// </summary>
        public bool IsEnableLoadBalance { get; set; }
    }
    public enum ServiceLevel
    {
        /// <summary>
        /// 
        /// </summary>
        Master,
        /// <summary>
        /// 工作节点
        /// </summary>
        Slave,
    }
}
