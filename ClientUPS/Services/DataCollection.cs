using ClientUPS.Models;

namespace ClientUPS.Services;

public static class DataCollection
{
    public static DataDeviceRequest Start()
    {
        return new DataDeviceRequest
        {
            SerialNumber = "26-REP-2023",
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
    }
}
