using UpsConnectService.Models.Users;
using UpsConnectService.ViewModels.Users;

namespace UpsConnectService.Extentions
{
    public static class UserFromModel
    {
        public static User Convert(this User user, UserEditViewModel usereditvm)
        {
            user.LastName = usereditvm.LastName;
            user.FirstName = usereditvm.FirstName;
            user.Email = usereditvm.Email;
            user.Company = usereditvm.Company;    
            
            return user;
        }
    }
}
