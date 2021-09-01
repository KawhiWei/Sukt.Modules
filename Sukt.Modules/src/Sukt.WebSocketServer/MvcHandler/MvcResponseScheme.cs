using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sukt.WebSocketServer.MvcHandler
{
    /// <summary>
    /// WebSocket通信响应方案
    /// WebSocket communication response scheme
    /// </summary>
    public class MvcResponseScheme
    {
        /// <summary>
        /// Response Id with request consistent 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Response status.
        /// Success:0,Application Error:1,NotFoundTarget:2
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Response message
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// Request time tick
        /// </summary>
        public long RequestTime { get; set; }

        /// <summary>
        /// Handle complate time tick
        /// </summary>
        public long ComplateTime { get; set; }

        /// <summary>
        /// Response body
        /// </summary>
        public object Body { get; set; }
    }
}
