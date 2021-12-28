
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Sukt.WebSocketServer.Configures
{
    /// <summary>
    /// WebSocket端点路由
    /// WebSocket endpoint
    /// </summary>
    public class WebSocketEndPoint
    {
        /// <summary>
        /// Controller Name
        /// </summary>
        public string Controller { get; set; }

        /// <summary>
        /// Action of controller
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// WebSocket request target
        /// </summary>
        public string MethodPath { get; set; }

        /// <summary>
        /// WebSocket Attribute method name
        /// </summary>
        public string[] Methods { get; set; }

        /// <summary>
        /// Method of action
        /// </summary>
        public MethodInfo MethodInfo { get; set; }

        /// <summary>
        /// Endpoint where class
        /// </summary>
        public Type Class { get; set; }
    }
}
