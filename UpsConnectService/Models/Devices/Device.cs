using UpsConnectService.Models.Users;

namespace UpsConnectService.Models.Devices
{
    public class Device
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public User? User { get; set; }
        public string? SerialNumber { get; set; }

    }
}
