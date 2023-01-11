using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UpsConnectService.Models;
using UpsConnectService.ViewModels;

namespace UpsConnectService.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }


    [Route("")]
    [Route("[controller]/[action]")]
    public IActionResult Index()
    {
        return View(new LoginViewModel());
    }

    [Route("[action]")]
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}