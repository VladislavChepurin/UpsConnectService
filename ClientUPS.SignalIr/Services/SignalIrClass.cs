using ClientUPS.SignalIr.Models;
using Microsoft.AspNetCore.SignalR.Client;

namespace ClientUPS.SignalIr.Services;

public class SignalIrClass
{
    public HubConnection? connection;

    public void InitSignalIr()
    {
        connection = new HubConnectionBuilder()
              .WithUrl("http://localhost:5192/ChatHub")
              .WithAutomaticReconnect()
              .Build();

        connection.Closed += async (error) =>
        {
            await Task.Delay(new Random().Next(0, 5) * 1000);
            await connection.StartAsync();
        };       
    }

    public async Task ConnectSignalIr()
    {
        connection?.On<string>("ResponseMessage", param => {
            Console.WriteLine(param);
        });

        try
        {
            if (connection != null)
                await connection.StartAsync();

        }           
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
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
