using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace Sukt.WebSocketServer
{
    public interface IWebSocketSession
    {
        /// <summary>
        /// 当前请求的HttpContext
        /// Current request http context
        /// </summary>
        public HttpContext WebSocketHttpContext { get; set; }

        /// <summary>
        /// 当前链接的WebSocket信息
        /// Current session web socket client
        /// </summary>
        public WebSocket WebSocketClient { get; set; }
    }
}
