﻿using AwesomeNetwork.Controllers.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UpsConnectService.Models.Users;
using UpsConnectService.ViewModels;
using UpsConnectService.ViewModels.Users;

namespace UpsConnectService.Controllers.Account
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        RoleManager<IdentityRole> _roleManager;
        UserManager<User> _userManager;
        public AdminController(ILogger<AdminController> logger, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _logger = logger;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [Route("Index")]
        public IActionResult Index() => View(   _userManager.Users.ToList());

        public IActionResult Create() => View();

        public IActionResult RoleList() => View(_roleManager.Roles.ToList());

        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(name);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await _roleManager.DeleteAsync(role);
            }
            return RedirectToAction("Index");
        }

        [Route("Edit")]
        [HttpGet]
        public async Task<IActionResult> Edit(string userId)
        {
            // получаем пользователя
            User user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                // получем список ролей пользователя
                var userRoles = await _userManager.GetRolesAsync(user);
                var allRoles = _roleManager.Roles.ToList();
                ChangeRoleViewModel model = new()
                {
                    UserId = user.Id,
                    UserEmail = user.Email,
                    UserRoles = userRoles,
                    AllRoles = allRoles
                };
                return View(model);
            }
            return NotFound();
        }

        [Route("Edit")]
        [HttpPost]
        public async Task<IActionResult> Edit(string userId, List<string> roles)
        {
            // получаем пользователя
            User user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                // получем список ролей пользователя
                var userRoles = await _userManager.GetRolesAsync(user);
                // получаем все роли
                var allRoles = _roleManager.Roles.ToList();
                // получаем список ролей, которые были добавлены
                var addedRoles = roles.Except(userRoles);
                // получаем роли, которые были удалены
                var removedRoles = userRoles.Except(roles);
                await _userManager.AddToRolesAsync(user, addedRoles);
                await _userManager.RemoveFromRolesAsync(user, removedRoles);

                _logger.LogInformation($"Изменены права доступа пользователя {user.UserName} ** {user.Email}");
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        [Route("DeleteUser")]
        [HttpGet]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            // получаем пользователя
            User user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
                _logger.LogInformation($"Удален пользователь {user.UserName} ** {user.Email}");
                return RedirectToAction("UserList");
            }
            return NotFound();
        }

        [Route("UserPage")]
        [HttpGet]
        public async Task<IActionResult> UserPage(string userId)
        {
            User user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var model = new UserViewModel(user);
                return View("User", model);
            }
            return NotFound();
        }
    }
}





