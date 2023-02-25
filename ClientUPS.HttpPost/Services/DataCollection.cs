using ClientUPS.HttpPost.Models;

namespace ClientUPS.HttpPost.Services;

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
                SerialNumber = "28-REP-2023",
                NameDevice = "РЭП-2000",
                StatusCode = new Random().Next(1, 10),
                InputVoltage = new Random().Next(150, 260),
                OutputVoltage = new Random().Next(180, 230),
                InputСurrent = 6.25,
                OutputCurrent = 5.14,
                BatteryCharge = 9.2,
                Bypass = false,
                GeneralError = true,
                BatteryError = false
            };
            if (MeasurementHandler != null)
                MeasurementHandler(models);
                       
            Thread.Sleep(4000);
        }

    }
}
