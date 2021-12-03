// See https://aka.ms/new-console-template for more information
using Sukt.Module.Core.Extensions;
using System.Net.WebSockets;
using System.Text;

Console.WriteLine("Hello, World!");
CancellationTokenSource CTS = new CancellationTokenSource(); ;
ClientWebSocket clientWebSocket = new ClientWebSocket();
await clientWebSocket.ConnectAsync(new Uri("ws://localhost:8002/im"), CancellationToken.None);
Console.WriteLine(clientWebSocket.State);

var msg = new WebSocketReceiveResult() { Id = Guid.NewGuid().ToString(), TargetAction = "im.login", Body = new WebSocketCloseStatusA() { Uid = "asdasdasdasdasdadsada" } };

var replyMess = Encoding.UTF8.GetBytes(msg.ToJson());
await clientWebSocket.SendAsync(new ArraySegment<byte>(replyMess), WebSocketMessageType.Text, true, CancellationToken.None);

int ReceiveBufferSize  = 8192;




await clientWebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);

//async Task ReceiveLoop()
//{
//    MemoryStream outputStream = null;
//    WebSocketReceiveResult receiveResult = null;
//    var buffer = new byte[ReceiveBufferSize];
//    try
//    {
//        while (!CTS.IsCancellationRequested)
//        {

//            outputStream = new MemoryStream(ReceiveBufferSize);
//            do
//            {
//                receiveResult = await clientWebSocket.ReceiveAsync(buffer, CancellationToken.None);
//                if (receiveResult.MessageType != WebSocketMessageType.Close)
//                    outputStream.Write(buffer, 0, receiveResult.Count);
//            }
//            while (!receiveResult.EndOfMessage);
//            if (receiveResult.MessageType == WebSocketMessageType.Close)
//                outputStream.Position = 0;
//            ResponseReceived(outputStream);
//        }
        
//    }
//    catch (TaskCanceledException) { }
//    finally
//    {
//        outputStream?.Dispose();
//    }
//}
//void ResponseReceived(Stream inputStream)
//{
//    // TODO: handle deserializing responses and matching them to the requests.
//    // IMPORTANT: DON'T FORGET TO DISPOSE THE inputStream!
//}
while (true)
{
    await Task.Delay(1000);
}


public class WebSocketReceiveResult
{
    public string Id { get; set; }
    public string TargetAction { get; set; }
    public WebSocketCloseStatusA Body { get; set; }
}
public class WebSocketCloseStatusA
{
    public string Uid { get; set; } 
}