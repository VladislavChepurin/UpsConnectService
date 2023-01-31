using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UpsConnectService.Data.Repositiry;
using UpsConnectService.Data.UoW;
using UpsConnectService.Models.Devices;
using UpsConnectService.Models.Users;
using UpsConnectService.ViewModels;
using UpsConnectService.ViewModels.Users;

namespace UpsConnectService.Controllers.Devices
{
    [Authorize(Roles = "Administrator, Operator")]
    public class RegisterDeviceController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;

        public RegisterDeviceController(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        [Route("RegisterNewDevice")]
        [HttpPost]
        public async Task<IActionResult> RegisterNewDevice(string userId, UserPageViewModel model)
        {
            var user = await _userManager.FindByIdAsync(userId);    

            if (ModelState.IsValid)
            {
                var repository = _unitOfWork.GetRepository<Device>() as DeviceRepository;

                repository?.AddDevice(user, model.RegisterViewsModel.NameDevices, model.RegisterViewsModel.SerialNumber);
                _unitOfWork.SaveChanges();
            }
            model = new UserPageViewModel
            {
                UserViewModel = new UserViewModel(user)
            };
            model.UserViewModel.LinkedDevices = _unitOfWork.GetAllDevices(user);
            return View("User", model);
        }
    }
}
