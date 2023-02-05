using System.ComponentModel.DataAnnotations.Schema;

namespace UpsConnectService.Models.Devices
{
    [Table("Devices")]

    public class DataDeviceRequest
    {
        public bool GeneralError { get; set; }
        public bool BatteryError { get; set; }
        public string? SerialNumber { get; set; }
        public string? NameDevice { get; set; }
        public int StatusCode { get; set; }
        public double InputVoltage { get; set; }
        public double OutputVoltage { get; set; }
        public double InputСurrent { get; set; }
        public double OutputCurrent { get; set; }
        public double BatteryCharge { get; set; }
        public bool Bypass { get; set; }

    }
}
