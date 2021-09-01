using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sukt.WebScoket.Configures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sukt.WebScoket
{
    public class WebSocketRouteOption
    {
        /// <summary>
        /// Dependency injection container provider
        /// DI容器提供者
        /// </summary>
        public static IServiceProvider ApplicationServices { get; set; }

        /// <summary>
        /// Dependency injection container
        /// DI容器
        /// </summary>
        public IServiceCollection ApplicationServiceCollection { get; set; }

        /// <summary>
        /// Injection HttpContext property name. 注入HttpContext属性名。
        /// Default property name: WebSocketHttpContext. 默认属性名称:WebSocketHttpContext。
        /// Injection property type: HttpContext. 注入属性类型:HttpContext
        /// 属性名称需要与IWebSocketSession接口内相同
        /// </summary>
        public string InjectionHttpContextPropertyName { get; set; } = "WebSocketHttpContext";

        /// <summary>
        /// Injection WebSocket property name. 注入WebSocket属性名。
        /// Default property name: WebSocketClient. 默认属性名称:WebSocketClient
        /// Injection property type: WebSocket. 注入属性类型:WebSocket
        /// 属性名称需要与IWebSocketSession接口内相同
        /// </summary>
        public string InjectionWebSocketClientPropertyName { get; set; } = "WebSocketClient";

        /// <summary>
        /// 通道处理器集合
        /// Channel handlers
        /// </summary>
        public Dictionary<string, WebSocketChannelHandler> WebSocketChannels { get; set; }
        /// <summary>
        /// 组装上下文
        /// </summary>
        public WatchAssemblyContext WatchAssemblyContext { get; set; }
        /// <summary>
        /// 程序集路径
        /// Watch assembly path
        /// </summary>
        public string WatchAssemblyPath { get; set; }
        /// <summary>
        /// 通道处理器
        /// </summary>
        /// <param name="context"></param>
        /// <param name="logger"></param>
        /// <param name="webSocketOptions"></param>
        /// <returns></returns>
        public delegate Task WebSocketChannelHandler(HttpContext context, ILogger<WebSocketRouteMiddleware> logger, WebSocketRouteOption webSocketOptions);
        /// <summary>
        /// Before establish connection handler
        /// </summary>
        /// <param name="context"></param>
        /// <param name="webSocketOptions"></param>
        /// <param name="channel"></param>
        /// <param name="logger"></param>
        /// <returns>true allow connection,false deny connection</returns>
        public delegate Task<bool> BeforeConnectionHandler(HttpContext context, WebSocketRouteOption webSocketOptions, string channel, ILogger<WebSocketRouteMiddleware> logger);
        /// <summary>
        /// 建立连接前呼叫
        /// Before establish connection call
        /// </summary>
        public event BeforeConnectionHandler BeforeConnectionEvent;
        /// <summary>
        /// BeforeConnectionEvent entry
        /// </summary>
        /// <param name="context"></param>
        /// <param name="webSocketOptions"></param>
        /// <param name="channel"></param>
        /// <param name="logger"></param>
        /// <returns></returns>
        public virtual Task<bool> OnBeforeConnection(HttpContext context, WebSocketRouteOption webSocketOptions, string channel, ILogger<WebSocketRouteMiddleware> logger)
        {
            if (BeforeConnectionEvent != null)
            {
                return BeforeConnectionEvent(context, webSocketOptions, channel, logger);
            }
            return Task.FromResult(true);
        }
        /// <summary>
        /// 关闭链接处理
        /// Close connectioned handler
        /// </summary>
        /// <param name="context"></param>
        /// <param name="webSocketOptions"></param>
        /// <param name="channel"></param>
        /// <param name="logger"></param>
        /// <returns></returns>
        public delegate Task DisConnectionedHandler(HttpContext context, WebSocketRouteOption webSocketOptions, string channel, ILogger<WebSocketRouteMiddleware> logger);
        /// <summary>
        /// 关闭链接事件处理
        /// </summary>
        public event DisConnectionedHandler DisConnectionedEvent;
        /// <summary>
        /// DisConnectionedEvent entry
        /// </summary>
        /// <param name="context"></param>
        /// <param name="webSocketOptions"></param>
        /// <param name="channel"></param>
        /// <param name="logger"></param>
        /// <returns></returns>
        public virtual Task OnDisConnectioned(HttpContext context, WebSocketRouteOption webSocketOptions, string channel, ILogger<WebSocketRouteMiddleware> logger)
        {
            if (DisConnectionedEvent != null)
            {
                return DisConnectionedEvent(context, webSocketOptions, channel, logger);
            }
            return Task.CompletedTask;
        }
    }
}
