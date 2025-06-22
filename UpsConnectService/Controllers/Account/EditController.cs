using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UpsConnectService.Data.Repositiry;
using UpsConnectService.Data.UoW;
using UpsConnectService.Extentions;
using UpsConnectService.Models.Devices;
using UpsConnectService.Models.Users;
using UpsConnectService.ViewModels;
using UpsConnectService.ViewModels.Users;

namespace UpsConnectService.Controllers.Account
{
    public class EditController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public EditController(UserManager<User> userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
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
        /// <param name="userEdit"></param>
        /// <returns></returns>
        [Authorize]
        [Route("Update")]
        [HttpPost]
        public async Task<IActionResult> Update(UserEditViewModel userEdit)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(userEdit.UserId);
                user.Convert(userEdit);     
                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    var model = new UserPageViewModel
                    {
                        UserViewModel = new UserViewModel(user)
                    };
                    model.UserViewModel.LinkedDevices = GetAllDevices(user);

                    return View("User", model);
                }
                else
                {
                    return RedirectToAction("EditUser");
                }
            }
            else
            {
                ModelState.AddModelError("", "Некорректные данные");
                return View("EditUser", userEdit);
            }
        }

        public List<DeviceUsers> GetAllDevices(User user)
        {
            var repository = _unitOfWork.GetRepository<DeviceUsers>() as DeviceUsersRepository;
            return repository!.getDeviceByUser(user);
        }
    }
}
