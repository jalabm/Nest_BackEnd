﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Nest_6._03.Enums;
using Nest_6._03.Models;
using Nest_6._03.ViewModels;

namespace Nest_6._03.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<AppUser> _signInManager;
    public AccountController(UserManager<AppUser> userManager,
                             RoleManager<IdentityRole> roleManager,
                             SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
    }

    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Login(LoginVm loginVm)
    {
        if (!ModelState.IsValid) return View(loginVm);

        var existUser = await _userManager.FindByEmailAsync(loginVm.Email);
        if (existUser == null)
        {
            ModelState.AddModelError("", "Invalid Credentials");
            return View();
        }

        var result = await _signInManager.PasswordSignInAsync(existUser, loginVm.Password, loginVm.RememberMe, true);
        if (!result.Succeeded)
        {
            ModelState.AddModelError("", "Invalid Credentials");
            return View();
        }
        var role = await _userManager.IsInRoleAsync(existUser, Roles.Admin.ToString());
        if (role)
            return RedirectToAction("Index", "Dashboard", new { Area = "Admin" });
        return RedirectToAction("Index", "Home");

    }
    public IActionResult Register()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Register(RegisterVm registerVm)
    {
        if (!ModelState.IsValid) return View(registerVm);

        var existUser = await _userManager.FindByNameAsync(registerVm.Username);
        if (existUser != null)
        {
            ModelState.AddModelError("", "User already exist");
            return View(registerVm);
        }
        AppUser newUser = new AppUser
        {
            Name = registerVm.Name,
            Surname = registerVm.Surname,
            Email = registerVm.Email,
            UserName = registerVm.Username,
        };
        var result = await _userManager.CreateAsync(newUser, registerVm.Password);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", $"{error.Code} - {error.Description}");
            }
            return View(registerVm);
        }

        if (registerVm.IsVendor)
        {
            var resultVendor = await _userManager.AddToRoleAsync(newUser, Roles.Vendor.ToString());
            if (!resultVendor.Succeeded)
            {
                foreach (var error in resultVendor.Errors)
                {
                    ModelState.AddModelError("", $"{error.Code} - {error.Description}");
                }
                return View(registerVm);
            }
        }
        else
        {
            var resultCustomer = await _userManager.AddToRoleAsync(newUser, Roles.Customer.ToString());
            if (!resultCustomer.Succeeded)
            {
                foreach (var error in resultCustomer.Errors)
                {
                    ModelState.AddModelError("", $"{error.Code} - {error.Description}");
                }
                return View(registerVm);
            }
        }
        return RedirectToAction("Index", "Home");
    }
    public async Task<IActionResult> LogOut()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
    //public async Task<IActionResult> CreateRole()
    //{
    //    foreach (var role in Enum.GetValues(typeof(Roles)))
    //    {
    //        await _roleManager.CreateAsync(new IdentityRole
    //        {
    //            Id = Guid.NewGuid().ToString(),
    //            Name = role.ToString(),
    //        });
    //    }
    //    return RedirectToAction("Index", "Home");
    //}
}

