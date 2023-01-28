using ServicePostServer.Models;

namespace ServicePostServer.Services;

public class DataCollection
{
    public delegate void Measurement(DataDeviceRequest dataDeviceRequest);

    public Measurement? MeasurementHandler { get; set; }

    public void Start()
    {
        while (true)
        {
            var models = new DataDeviceRequest
            {
                SerialNumber = "A001-test",
                NameDevice = "РЭП-2000",
                StatusCode = new Random().Next(0, 10),
                InputVoltage = new Random().Next(200, 240),
                OutputVoltage = new Random().Next(220, 225),
                InputСurrent = 6.25,
                OutpuCurrent = 5.14,
                BatteryCharge = 9.2,
                Bypass = false,
                GeneralError = true,
                BatteryError = false
            };
            if (models is not null)
            {
                MeasurementHandler(models);
            }
           
            Thread.Sleep(10000);
        }

    }
}
