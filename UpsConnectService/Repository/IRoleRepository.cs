using UpsConnectService.Models.Users;

namespace UpsConnectService.Repository
{
    public interface IRoleRepository
    {
        Task CreateInitRoles();
        Task AssignRoles(User user, string codeRole);

    }
}
