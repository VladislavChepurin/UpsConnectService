using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UpsConnectService.Models.Users;
using UpsConnectService.Repository;
using UpsConnectService.ViewModels;

namespace AwesomeNetwork.Controllers.Account;

public class RegisterController : Controller
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IRoleRepository _roleRepository;   

    public RegisterController(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, IRoleRepository roleRepository)
    {
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
        _roleRepository = roleRepository;

    }
    [Route("Register")]
    [HttpGet]
    public IActionResult Register()
    {
        return View("RegisterUser");
    }


    [Route("Register")]
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {

            var user = _mapper.Map<User>(model);           
            var result = await _userManager.CreateAsync(user, model.PasswordReg);
            if (result.Succeeded)
            {
                await _roleRepository.CreateInitRoles();
                await _roleRepository.AssignRoles(user, model.CodeRegister ?? String.Empty);

                await _signInManager.SignInAsync(user, false);            
                return View("ServiceView");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
        }
        return View("RegisterUser", model);
    }
}
