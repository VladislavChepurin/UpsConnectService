using ClientUPS.HttpPost.Models;
using ClientUPS.HttpPost.Services;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace ClientUPS.HttpPost;
internal class Program
{
    private static HttpClient? sharedClient;


    static void Main(string[] args)
    {
        HttpClientHandler clientHandler = new()
        {
            ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
        };

        sharedClient = new(clientHandler)
        {
            Timeout = TimeSpan.FromSeconds(5),
            BaseAddress = new Uri("https://localhost:7252"),
        };

        var getRate = new DataCollection
        {
            MeasurementHandler = DataPostRequest
        };
        getRate.Start();
    }


    private static async void DataPostRequest(DataDeviceRequest dataDeviceRequest)
    {
        string json = JsonConvert.SerializeObject(dataDeviceRequest);
        if (sharedClient != null)
            await PostAsync(sharedClient, json);      
    }

    static async Task PostAsync(HttpClient httpClient, string json)
    {
        using StringContent jsonContent = new(json, Encoding.UTF8, "application/json");      

        try
        {    
            // using HttpResponseMessage response = await httpClient.PostAsync("DeviceServices", jsonContent);
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "DeviceServices")
            {
                Version = HttpVersion.Version30,
                Content = jsonContent
            };

            using HttpResponseMessage response = await httpClient.SendAsync(httpRequestMessage);

            Console.WriteLine(await response.Content.ReadAsStringAsync());
        }
        catch (HttpRequestException)
        {
            Console.WriteLine($"Disconect {DateTime.Now:G}");
        }
        catch (TaskCanceledException)
        {
            Console.WriteLine($"Timeout {DateTime.Now:G}");
        }
    }
}