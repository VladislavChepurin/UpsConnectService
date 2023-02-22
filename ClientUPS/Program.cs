using System.Net.WebSockets;
using System.Text;

internal class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("press enter to continue");
        Console.ReadLine();

        using var ws = new ClientWebSocket();
        var cTs = new CancellationTokenSource();
        cTs.CancelAfter(TimeSpan.FromSeconds(120));
        try
        {
            await ws.ConnectAsync(new Uri("ws://localhost:5192/send"), CancellationToken.None);

            while (ws.State == WebSocketState.Open)
            {
                Console.WriteLine("Enter message to send");
                string message = Console.ReadLine() ?? "empty message";

                if (ws.State != WebSocketState.Open)
                    break;

                ArraySegment<byte> byteSends = new(Encoding.UTF8.GetBytes(message));
                await ws.SendAsync(byteSends, WebSocketMessageType.Text, true, CancellationToken.None);
                var responseBuffer = new byte[1024];
                var offset = 0;
                var packet = 1024;
                while (true)
                {
                    ArraySegment<byte> byteRecieved = new(responseBuffer, offset, packet);
                    WebSocketReceiveResult respons = await ws.ReceiveAsync(byteRecieved, CancellationToken.None);
                    var responseMessage = Encoding.UTF8.GetString(responseBuffer, offset, respons.Count);
                    Console.WriteLine(responseMessage);
                    if (respons.EndOfMessage)
                        break;
                }                
            }
        }
        catch (WebSocketException e)
        {
            Console.WriteLine(e.Message);
        }

        catch (TaskCanceledException e)
        {
            Console.WriteLine(e.Message);
        }

        Console.ReadLine();
    }
}