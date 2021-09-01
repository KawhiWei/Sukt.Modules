using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sukt.WebScoket.MvcHandler
{
    /// <summary>
    /// Websocket传入方案
    /// WebSocket communication scheme
    /// </summary>
    public class MvcRequestScheme
    {
        /// <summary>
        /// 请求Id
        /// Request Id
        /// 在多路复用中需要保证Id的唯一性
        /// In Multiplex, you need to keep the id of uniqueness
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Request target
        /// </summary>
        public string TargetAction { get; set; }

        /// <summary>
        /// Request context
        /// </summary>
        public object Body { get; set; }
    }
}
