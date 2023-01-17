using System.ComponentModel.DataAnnotations.Schema;

namespace UpsConnectService.Models.Device
{
    [Table("Devices")]
    public class Devices
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Message { get; set; }
    }
}
