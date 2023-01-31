using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UpsConnectService.Data;
using UpsConnectService.Data.UoW;
using UpsConnectService.Models.Users;
using UpsConnectService.ViewModels;
using UpsConnectService.ViewModels.Users;

namespace UpsConnectService.Controllers.Account;

public class AccountController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public AccountController(ApplicationDbContext context, UserManager<User> userManager, SignInManager<User> signInManager, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context;
        _unitOfWork = unitOfWork;
    }

    [Authorize]
    public IActionResult AccessDenied()
    {
        return View();
    }

    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> LoginAsync(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var userData = _context.Users.FirstOrDefault(p => p.Email == model.Email);
            var result = await _signInManager.PasswordSignInAsync(userData?.UserName ?? String.Empty, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                {
                    return Redirect(model.ReturnUrl);
                }
                else
                {
                    return RedirectToAction("ServiceView", "Account");
                }
            }
            else
            {
                ModelState.AddModelError("", "Неправильный логин и (или) пароль");
            }
        }
        return View("~/Views/Home/Index.cshtml", new LoginViewModel());
    }

    [Authorize]
    [Route("ServiceView")]
    [HttpGet]
    public async Task<IActionResult> ServiceView()
    {
        var user = User;
        var result = await _userManager.GetUserAsync(user);
        var model = new UserViewModel(result);
        model.LinkedDevices = _unitOfWork.GetAllDevices(model.User);
        return View("ServiceView", model);
    }

    [Route("Logout")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    [Authorize]
    [HttpPost]
    [Route("UserPage")]  
    public async Task<IActionResult> UserPage()
    {
        var userClaims = User;
        var user = await _userManager.GetUserAsync(userClaims);
        var model =new UserPageViewModel
        { 
            UserViewModel = new UserViewModel(user)
        };
        model.UserViewModel.LinkedDevices = _unitOfWork.GetAllDevices(user);
        return View("User", model);
    }

    [HttpGet]
    [Route("Login")]
    public IActionResult Login()
    {
        return View("Login");
    }

    public IActionResult Index()
    {
        return View();
    }
}
