using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using System.Text;
using UpsConnectService.Models.Users;

namespace UpsConnectService.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ILogger<RoleRepository> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        List<string> roles = new List<string>() {
            "Administrator",
            "Operator",
            "User",
            "Outher"
        };

        public RoleRepository(ILogger<RoleRepository> logger, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _logger= logger;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task CreateInitRoles()
        {
            if (_roleManager.Roles.ToList().Count == 0)
            {
                foreach (var role in roles)
                {
                    IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(role));
                    if (result.Succeeded)
                    {
                        _logger.LogInformation($"Создана роль {role}");
                    }
                }
            }
        }

        public async Task AssignRoles(User user, string codeRole)
        {
            string output; //дайджест сообщения (хэш)

            MD5 MD5Hash = MD5.Create(); //создаем объект для работы с MD5
            byte[] inputBytes = Encoding.ASCII.GetBytes(codeRole); //преобразуем строку в массив байтов
            byte[] hash = MD5Hash.ComputeHash(inputBytes); //получаем хэш в виде массива байтов
            output = Convert.ToHexString(hash); //преобразуем хэш из массива в строку, состоящую из шестнадцатеричных символов в верхнем регистре        

            switch (output)
            {
                case "A9E6FD4D8A7772193EA1C94D11A5AEC8": // Role: Administrator + Operator + User + Outher    
                    if ((await _userManager.AddToRoleAsync(user, roles[0])).Succeeded)
                    {
                        _logger.LogInformation($"Пользователю {user.UserName} присвоена роль {roles[0]}");
                    }

                    if ((await _userManager.AddToRoleAsync(user, roles[1])).Succeeded)
                    {
                        _logger.LogInformation($"Пользователю {user.UserName} присвоена роль {roles[1]}");
                    }

                    if ((await _userManager.AddToRoleAsync(user, roles[2])).Succeeded)
                    {
                        _logger.LogInformation($"Пользователю {user.UserName} присвоена роль {roles[2]}");
                    }

                    if ((await _userManager.AddToRoleAsync(user, roles[3])).Succeeded)
                    {
                        _logger.LogInformation($"Пользователю {user.UserName} присвоена роль {roles[3]}");
                    }
                    break;

                case "108550CC9160AB741E3F6CC54E3C2AAF": // Role: Operator + User + Outher
                    if ((await _userManager.AddToRoleAsync(user, roles[1])).Succeeded)
                    {
                        _logger.LogInformation($"Пользователю {user.UserName} присвоена роль {roles[1]}");
                    }

                    if ((await _userManager.AddToRoleAsync(user, roles[2])).Succeeded)
                    {
                        _logger.LogInformation($"Пользователю {user.UserName} присвоена роль {roles[2]}");
                    }

                    if ((await _userManager.AddToRoleAsync(user, roles[3])).Succeeded)
                    {
                        _logger.LogInformation($"Пользователю {user.UserName} присвоена роль {roles[3]}");
                    }
                    break;

                case "FB297B1F10660C5E0852E2128C809A61": // Role: User + Outher
                    if ((await _userManager.AddToRoleAsync(user, roles[2])).Succeeded)
                    {
                        _logger.LogInformation($"Пользователю {user.UserName} присвоена роль {roles[2]}");
                    }

                    if ((await _userManager.AddToRoleAsync(user, roles[3])).Succeeded)
                    {
                        _logger.LogInformation($"Пользователю {user.UserName} присвоена роль {roles[3]}");
                    }
                    break;

                default:                                 // Role: Outher
                    if ((await _userManager.AddToRoleAsync(user, roles[3])).Succeeded)
                    {
                        _logger.LogInformation($"Пользователю {user.UserName} присвоена роль {roles[3]}");
                    }
                    break;
            }
        }
    }
}
