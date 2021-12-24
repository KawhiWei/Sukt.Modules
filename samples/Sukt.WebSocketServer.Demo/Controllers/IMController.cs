using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sukt.Module.Core.Extensions;
using Sukt.WebSocketServer.Attributes;
using Sukt.WebSocketServer.MvcHandler;
using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.WebSocketServer.Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IMController : ControllerBase, IWebSocketSession
    {
        public HttpContext WebSocketHttpContext { get ; set ; }
        public WebSocket WebSocketClient { get ; set; }
        /// <summary>
        /// Login
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        [WebSocket]
        [HttpPost]
        public async Task<string> Login(string uid)
        {
            await Task.CompletedTask;
            //登录
            //把ContextId与uid关联

            Console.WriteLine($"12132as1d32sa1d3as1d32{uid}---------->{WebSocketHttpContext.Connection.Id}");
            Console.WriteLine(MvcChannelHandler.Clients);


            //var msg = new { imsgd = "asdasdasdasdasdadsa啊实打实打算打赏阿斯顿阿斯顿阿斯顿撒旦阿萨阿萨da" };

            //var replyMess = Encoding.UTF8.GetBytes(msg.ToJson());
            //await WebSocketClient.SendAsync(new ArraySegment<byte>(replyMess), WebSocketMessageType.Text, true, CancellationToken.None);
            return "ad143as1d3asd3as1d3as23d32aas";//此处返回的数据会自动发送给WebSocket客户端，无需手动发送
        }
    }
}
