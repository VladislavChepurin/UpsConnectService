using ClientUPS.SignalIr.Models;
using Microsoft.AspNetCore.SignalR.Client;
using System.Diagnostics;

namespace ClientUPS.SignalIr.Services;

public class SignalIrClass
{
    public HubConnection? connection;

    public async Task InitSignalIr()
    {
        connection = new HubConnectionBuilder()
              .WithUrl("http://localhost:5192/ChatHub")
              .WithAutomaticReconnect(new[] { TimeSpan.Zero, TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(20), TimeSpan.FromSeconds(30) })
              .Build();

        connection.Reconnecting += error =>
        {
            Console.WriteLine($"Reconect {DateTime.Now:G}");
            return Task.CompletedTask;
        };

        connection.Reconnected += connectionId =>
        {
            Console.WriteLine($"Reconect is OK {DateTime.Now:G}");
            return Task.CompletedTask;
        };

        connection.Closed += async (error) =>
        {
            await ConnectSignalIr(connection);
        };

        connection?.On<string>("ResponseMessage", param => {
            Console.WriteLine(param);
        });

        if(connection != null)
            await ConnectSignalIr(connection);
    }

    public static async Task ConnectSignalIr(HubConnection connection)
    {
        while (connection?.State != HubConnectionState.Connected)
        {
            try
            {
                if (connection != null)
                    await connection.StartAsync();
            }
            catch (Exception)
            {
                Console.WriteLine($"Disconect {DateTime.Now:G}");
            }
            Thread.Sleep(10000);
        }
    }

    public async Task SendMessages(DataDeviceRequest request)
    {
        try
        {
            if (connection != null)
                await connection.InvokeAsync("SendMessage", request);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
