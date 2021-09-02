using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sukt.WebSocketServer.MvcHandler
{
    /// <summary>
    /// WebSocket处理器接口
    /// </summary>
    public interface IWebSocketHandler
    {
        /// <summary>
        /// Handler metadata
        /// </summary>
        WebSocketHandlerMetadata Metadata { get; }
        /// <summary>
        /// Text transfer buffer size
        /// </summary>
        int ReceiveTextBufferSize { get; set; }
        /// <summary>
        /// Binary transfer buffer size
        /// 二进制大小
        /// </summary>
        int ReceiveBinaryBufferSize { get; set; }

        /// <summary>
        /// Connection request entry
        /// </summary>
        /// <param name="context"></param>
        /// <param name="logger"></param>
        /// <param name="webSocketOptions"></param>
        /// <returns></returns>
        Task ConnectionEntry(HttpContext context, ILogger<WebSocketRouteMiddleware> logger, WebSocketRouteOption webSocketOptions);
    }
    /// <summary>
    /// WebSocketHandler describe
    /// WebSocketHandler描述
    /// </summary>
    public class WebSocketHandlerMetadata
    {
        /// <summary>
        /// Describe the function of the handle and how to use it 
        /// 描述hanlder的功能和使用方法
        /// </summary>
        public string Describe { get; set; }

        /// <summary>
        /// This handle allows binary to be transferred
        /// 这个hanlder允许二进制文件被传输
        /// </summary>
        public bool CanHandleBinary { get; set; }

        /// <summary>
        /// This handle allows text to be transferred
        /// 这个hanlder允许文本传输
        /// </summary>
        public bool CanHandleText { get; set; }
    }
}
