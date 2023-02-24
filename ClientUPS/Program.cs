﻿using ClientUPS.Models;
using ClientUPS.Services;
using System.Net.WebSockets;

internal class Program
{  
    static async Task Main(string[] args)
    {
        Uri uri = new Uri("ws://localhost:5192/send");
        WebSocetsClass webSocketClass = new(uri);

        var client = new ClientWebSocket();

        while (true)
        {
            try
            {
                if (client.State != WebSocketState.Open)
                {
                    await webSocketClass.ConectWebSocet(client);
                }
                else
                {
                    await webSocketClass.SendJson(DataCollection.Start(), client);
                    await webSocketClass.ResponseMessage(client);
                }
            }
            catch (WebSocketException)
            {
                if (client.State == WebSocketState.Closed)
                    Console.WriteLine("Disconect");
                client.Dispose();
                client = new ClientWebSocket();
            }

            catch (IOException)
            {
                Console.WriteLine("Disconect");
                client.Dispose();
                client = new ClientWebSocket();
            }
            Thread.Sleep(4000);
        }
    }
}