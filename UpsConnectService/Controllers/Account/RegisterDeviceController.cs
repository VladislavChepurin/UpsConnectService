using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UpsConnectService.Models.Users;
using UpsConnectService.ViewModels;
using UpsConnectService.ViewModels.Users;

namespace UpsConnectService.Controllers.Account
{
    [Authorize(Roles = "Administrator, Operator")]
    public class RegisterDeviceController : Controller
    {

        [Route("RegisterNewDevice")]
        [HttpPost]
        public async Task<IActionResult> RegisterNewDevice(UserPageViewModel model)
        {
            if (ModelState.IsValid)
            {
                


            }


            return View(model);
        }

    }
}
