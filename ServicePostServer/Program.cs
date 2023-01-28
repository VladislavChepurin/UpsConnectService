using Newtonsoft.Json;
using ServicePostServer.Models;
using ServicePostServer.Serializable;
using ServicePostServer.Services;
using System.Text;

internal class Program
{
    
    static void Main(string[] args)
    {
        var getRate = new DataCollection
        {
            MeasurementHandler = DataPostRequest
        };
        getRate.Start();    
    }

    private static HttpClient sharedClient = new()
    {
        BaseAddress = new Uri("https://localhost:5001"),
    };

    private static string GetServerUrl()
    {
        string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config/appSettings.json");
        AppSettingsDeserialize app = new(jsonFilePath);
        var settings = app.GetSettingsModels();
        return settings.ServerUrl;
    }

    private static async void DataPostRequest(DataDeviceRequest dataDeviceRequest)
    {  
        string json = JsonConvert.SerializeObject(dataDeviceRequest);
        await PostAsync(sharedClient, json);
    }
    
    static async Task PostAsync(HttpClient httpClient, string json)
    {
        using StringContent jsonContent = new(json, Encoding.UTF8, "application/json");

        try
        {
            using HttpResponseMessage response = await httpClient.PostAsync("DeviceServices", jsonContent);
            Console.WriteLine(response.EnsureSuccessStatusCode().RequestMessage);

        }
        catch (HttpRequestException)
        {
            Console.WriteLine("Произошла ошибка при отправке запроса");
        }
    }
}