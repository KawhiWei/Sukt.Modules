using Sukt.Module.Core.Extensions;
using System.Net.WebSockets;
using System.Text;

namespace Sukt.WebSocket.Client
{
    public class Testaaa
    {
        private static ClientWebSocket clientWebSocket;
        private static CancellationTokenSource CTS;
        static int ReceiveBufferSize = 8 * 1024;
        public static async Task SendAsync()
        {
            CTS = new CancellationTokenSource();
            Console.WriteLine("Hello, World!");
            clientWebSocket = new ClientWebSocket();
            await clientWebSocket.ConnectAsync(new Uri("ws://localhost:5000/im"), CancellationToken.None);
            Console.WriteLine(clientWebSocket.State);

            var msg = new { id = Guid.NewGuid().ToString(), targetAction = "im.login", body = new { uid = "asdasdasdasdasdadsada" } };
            var replyMess = Encoding.UTF8.GetBytes(msg.ToJson());
            await clientWebSocket.SendAsync(new ArraySegment<byte>(replyMess), WebSocketMessageType.Text, true, CancellationToken.None);//监听Socket信息
            //全部消息容器
            List<byte> bs = new List<byte>();
            //缓冲区
            var buffer = new byte[1024 * 4];
            WebSocketReceiveResult result = await clientWebSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            //是否关闭
            while (!result.CloseStatus.HasValue)
            {
                //文本消息
                if (result.MessageType == WebSocketMessageType.Text)
                {
                    bs.AddRange(buffer.Take(result.Count));
                    //消息是否已接收完全
                    if (result.EndOfMessage)
                    {
                        //发送过来的消息
                        string userMsg = Encoding.UTF8.GetString(bs.ToArray(), 0, bs.Count);
                        Console.WriteLine(userMsg);
                        //清空消息容器
                        bs = new List<byte>();
                    }
                    //继续监听Socket信息
                    result = await clientWebSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                }
            }


        }
        public class WebSocketReceive
        {
            public string Id { get; set; }
            public string TargetAction { get; set; }
            public object Body { get; set; }
        }
        public class WebSocketCloseStatusA
        {
            public string Uid { get; set; }
        }
    }
}
