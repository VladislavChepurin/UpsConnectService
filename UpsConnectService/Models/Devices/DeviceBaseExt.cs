namespace UpsConnectService.Models.Devices;

public class DeviceBaseExt : DataDeviceRequest
{
    public int Id { get; set; }
    public DateTime  DateTime { get; set; } = DateTime.Now;
}
