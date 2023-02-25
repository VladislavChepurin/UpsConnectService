using ClientUPS.WebSocket.Models;
using Newtonsoft.Json;
using System.Net.WebSockets;
using System.Text;

namespace ClientUPS.WebSocket.Services
{
    internal class WebSocetsClass
    {
        private Uri Uri { get; set; }
        private CancellationToken Ct;

        public WebSocetsClass(Uri uri, CancellationToken ct = default(CancellationToken))
        {
            Uri = uri; 
            Ct = ct;
        }             

        public async Task ConectWebSocet(ClientWebSocket clientWebSocket)
        {
            await clientWebSocket.ConnectAsync(Uri, CancellationToken.None);
        }

        public async Task ResponseMessage(ClientWebSocket clientWebSocket)
        {                
            var responseBuffer = new byte[1024];
            var offset = 0;
            var packet = 1024;
            while (true)
            {
                ArraySegment<byte> byteRecieved = new(responseBuffer, offset, packet);
                WebSocketReceiveResult respons = await clientWebSocket.ReceiveAsync(byteRecieved, CancellationToken.None);
                var responseMessage = Encoding.UTF8.GetString(responseBuffer, offset, respons.Count);
                Console.WriteLine(responseMessage);
                if (respons.EndOfMessage)
                    break;
            }
        }

        public async Task SendJson(DataDeviceRequest dataDeviceRequest, ClientWebSocket clientWebSocket)
        {         
            using var stream = new MemoryStream();
            WriteJsonToStream(stream, dataDeviceRequest);
            await clientWebSocket.SendAsync(new ArraySegment<byte>(stream.ToArray()), WebSocketMessageType.Text, true, Ct);
        }

        private static void WriteJsonToStream(Stream stream, DataDeviceRequest dataDeviceRequest)
        {
            using var streamWriter = new StreamWriter(stream);
            using var writer = new JsonTextWriter(streamWriter);
            JsonSerializer serializer = new JsonSerializer();
            //serializer.Converters.Add(new JavaScriptDateTimeConverter());
            //serializer.NullValueHandling = NullValueHandling.Ignore;
            serializer.Serialize(writer, dataDeviceRequest);
        }

        public async Task SendClose(ClientWebSocket clientWebSocket, WebSocketCloseStatus status = WebSocketCloseStatus.NormalClosure, string? message = null)
        {
            await clientWebSocket.CloseAsync(status, message, Ct);
        }               
    }
}
