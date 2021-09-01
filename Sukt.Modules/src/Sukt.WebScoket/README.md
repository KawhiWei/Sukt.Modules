# 快速使用
## 1.Install Nuget
> 安装Nuget包 Sukt.WebScoket
## 2、ConfigureServices
``` c#
service.AddSuktWebSocketConfigRouterEndpoint(x =>
            {
                x.WebSocketChannels = new Dictionary<string, WebSocketRouteOption.WebSocketChannelHandler>()
                {
                    { "/im",new MvcChannelHandler(4*1024).MvcChannel_Handler}
                };
                x.ApplicationServiceCollection = service;
            });
```
## 3、Configure 引入中间件Middware
```c#
var webSocketOptions = new WebSocketOptions()
{
    KeepAliveInterval = TimeSpan.FromSeconds(120),
    ReceiveBufferSize = 4 * 1024
};
app.UseWebSockets(webSocketOptions);
app.UseSuktWebSocketServer(app.ApplicationServices);
```
## 4、使用
需要继承**IWebSocketSession**接口用来标，和配置**WebSocket**特性
```c#
[Route("api/[controller]")]
    [ApiController]
    public class IMController : ControllerBase, IWebSocketSession 
    {
        public HttpContext WebSocketHttpContext { get ; set ; }
        public WebSocket WebSocketClient { get ; set ; }
        /// <summary>
        /// Login
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        [WebSocket]
        [HttpPost("Login")]
        public async Task<string> Login(string uid)
        {

            Console.WriteLine($"12132as1d32sa1d3as1d32{uid}---------->{HttpContext.Connection.Id}");
            return HttpContext.Connection.Id;
        }
    }
```
## 5、前端调用
```typescript
  const ws = useRef<WebSocket | null>(null);
  const [message, setMessage] = useState('');
  /*
   * 链接服务端
   */
  const conncetionWebSocket = () => {
    ws.current = new WebSocket('ws://localhost:8002/im');
    ws.current.onmessage = e => {
      setMessage(e.data)
    }
    ws.current.onopen = () => {
      console.log("链接成功了")
    }
    return () => {
      ws.current?.close();
    };
  }
  /*
   * 发送消息
   */
  const websocketsend = () => {
      //消息处理标准结构
      var msg = {
      id: 'login',
      TargetAction: "im.login",
      body: { 
        uid: "asdasdasdasdasdadsada" 
      }
    };
    console.log(ws.current);
    ws.current?.send(JSON.stringify(msg))
  }
```
> 