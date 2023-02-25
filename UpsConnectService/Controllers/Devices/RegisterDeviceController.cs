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
                var repository = _unitOfWork.GetRepository<DeviceUsers>() as DeviceUsersRepository;
                if (repository != null)
                    repository.AddDevice(user, model.RegisterViewsModel.NameDevices, model.RegisterViewsModel.SerialNumber);
                _unitOfWork.SaveChanges();
            }
            model = new UserPageViewModel
            {
                UserViewModel = new UserViewModel(user)
            };
            model.UserViewModel.LinkedDevices = GetAllDevices(user);
            return View("User", model);
        }

        [Route("DeleteDevice")]
        [HttpPost]
        public async Task<IActionResult> DeleteDevice(string serialNumber, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);           
            if (user != null)
            {
                var repository = _unitOfWork.GetRepository<DeviceUsers>() as DeviceUsersRepository;
                repository?.DeleteDevice(user, serialNumber);
                _unitOfWork.SaveChanges();

                var model = new UserPageViewModel
                {
                    UserViewModel = new UserViewModel(user)
                };
                model.UserViewModel.LinkedDevices = GetAllDevices(user);
                return View("User", model);
            }
            return NotFound();
        }

        public List<DeviceUsers> GetAllDevices(User user)
        {
            var repository = _unitOfWork.GetRepository<DeviceUsers>() as DeviceUsersRepository;
            return repository.getDeviceByUser(user);
        }
    }
}
