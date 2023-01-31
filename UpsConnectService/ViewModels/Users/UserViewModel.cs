using UpsConnectService.Models.Devices;
using UpsConnectService.Models.Users;

namespace UpsConnectService.ViewModels.Users
{
    public class UserViewModel
    {
        public User User { get; set; }

        public List<Device> LinkedDevices { get; set; }

        public UserViewModel(User user)
        {
            User = user;
        }
    }
}
