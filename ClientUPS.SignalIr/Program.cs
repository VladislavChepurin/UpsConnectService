using ClientUPS.SignalIr.Services;
using ClientUPS.WebSocket.Services;
using Microsoft.AspNetCore.SignalR.Client;

namespace ClientUPS.SignalIr;

internal class Program
{
 
    static async Task Main(string[] args)
    {
        var signalIrClass = new SignalIrClass();
        await signalIrClass.InitSignalIr();
        Task.Delay(500).Wait();
       // await signalIrClass.ConnectSignalIr();

        while (true)
        {
            if (signalIrClass?.connection?.State == HubConnectionState.Connected)
            {
                await signalIrClass.SendMessages(DataCollection.Start());
            }           
            Thread.Sleep(4000);
        }
    }
}