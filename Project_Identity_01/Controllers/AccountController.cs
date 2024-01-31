﻿using Azure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Project_Identity_01.Models;
using Project_Identity_01.Models.ViewModels;
using System.Diagnostics;

namespace Project_Identity_01.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public AccountController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Register()
        {
            RegisterVM registerVm = new RegisterVM();
            return View(registerVm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM vm)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = vm.Email, //bh 
                    Email = vm.Email,
                    Name = vm.Name
                };
                var result =await _userManager.CreateAsync(user,vm.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user,isPersistent:false);
                    return RedirectToAction("Index","Home");
                }
            }
            return View(vm);
        }

    }
}