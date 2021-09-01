using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sukt.WebSocketServer
{
    public class WebSocketRouteMiddleware
    {
        private readonly RequestDelegate _next;
        private ILogger<WebSocketRouteMiddleware> _logger { get; }
        private readonly WebSocketRouteOption _webSocketRouteOption;

        public WebSocketRouteMiddleware(RequestDelegate next, ILogger<WebSocketRouteMiddleware> logger, WebSocketRouteOption webSocketRouteOption)
        {
            _next = next;
            _logger = logger;
            _webSocketRouteOption = webSocketRouteOption;
        }
        public async Task Invoke(HttpContext context)
        {
            bool hasHandler = _webSocketRouteOption.WebSocketChannels.TryGetValue(context.Request.Path, out var handler);
            if(hasHandler)
            {
                await handler(context, _logger, _webSocketRouteOption);
                return;
            }
            await _next(context);
        }
    }
}
