using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UpsConnectService.Data;
using UpsConnectService.Models.Users;
using UpsConnectService.ViewModels;
using UpsConnectService.ViewModels.Users;

namespace UpsConnectService.Controllers.Account;

public class AccountController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ApplicationDbContext _context;

    public AccountController(ApplicationDbContext context, UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context;
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
            var result = await _signInManager.PasswordSignInAsync(userData?.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);

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
