using UpsConnectService.Models.Devices;
using UpsConnectService.Models.Users;

namespace UpsConnectService.ViewModels.Users
{
    public class UserViewModel
    {
        public User User { get; set; }

        public List<Device>? LinkedDevices { get; set; }

        private int number = default;
        public int Number
        {
            get
            {
                return ++number;    // возвращаем значение свойства
            }           
        }

        public UserViewModel(User user)
        {
            User = user;
        }    

    }
}
