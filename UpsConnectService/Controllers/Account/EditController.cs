using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UpsConnectService.Data;
using UpsConnectService.Models.Users;
using UpsConnectService.ViewModels.Users;
using Microsoft.AspNetCore.Authorization;
using UpsConnectService.Extentions;

namespace UpsConnectService.Controllers.Account
{
    public class EditController : Controller
    {
        private readonly UserManager<User> _userManager;

        public EditController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Редактирование пользователя
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [Route("EditUser")]
        [HttpPost]
        public async Task<IActionResult> EditUser(string userId)
        {
            User user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                return View("EditUser", new UserEditViewModel(user));
            }
            return NotFound();
        }

        /// <summary>
        /// Обновление пользователя
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize]
        [Route("Update")]
        [HttpPost]
        public async Task<IActionResult> Update(UserEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.UserId);
                user.Convert(model);

                var userView = new UserViewModel(user);

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return View("User", userView);
                }
                else
                {
                    return RedirectToAction("EditUser");
                }
            }
            else
            {
                ModelState.AddModelError("", "Некорректные данные");
                return View("EditUser", model);
            }
        }
    }
}
