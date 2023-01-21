using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UpsConnectService.Models.Devices
{
    [Table("Devices")]

    public class DataDeviceRequest
    {
       // public Guid Id { get; set; } = Guid.NewGuid();
        public string? SerialNumber { get; set; }
        public string? NameDevice { get; set; }
        public int StatusCode { get; set; }
        public float InputVoltage { get; set; }
        public float OutputVoltage { get; set; }
        public float InputСurrent { get; set; }
        public float OutpuCurrent { get; set; }
        public float BatteryCharge { get; set; }
        public bool Bypass { get; set; }
        public bool GeneralError { get; set; }
        public bool BatteryError { get; set; }
    }
}
