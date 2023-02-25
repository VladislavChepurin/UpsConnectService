using ClientUPS.SignalIr.Services;
using ClientUPS.WebSocket.Services;

namespace ClientUPS.SignalIr;

internal class Program
{
    static async Task Main(string[] args)
    {
        var signalIrClass = new SignalIrClass();
        signalIrClass.InitSignalIr();
        Task.Delay(500).Wait();
        await signalIrClass.ConnectSignalIr();

        while (true)
        {
            await signalIrClass.SendMessages(DataCollection.Start());
            Thread.Sleep(4000);
        }
    }






}