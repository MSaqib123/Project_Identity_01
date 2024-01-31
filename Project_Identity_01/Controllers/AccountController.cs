using Azure.Identity;
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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        //======================================
        //============ Register ================
        //======================================
        #region Register
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
                //__________ Applicaiton User ___________
                var user = new ApplicationUser
                {
                    UserName = vm.Email, //bh 
                    Email = vm.Email,
                    Name = vm.Name
                };

                //__________ Identity User ___________
                //var user = new IdentityUser
                //{
                //    UserName = vm.Email, //bh 
                //    Email = vm.Email
                //};

                var result = await _userManager.CreateAsync(user, vm.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }
            return View(vm);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        #endregion

        //======================================
        //============ Logout ================
        //======================================
        #region Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        #endregion

        //======================================
        //============ Login ==================
        //======================================
        #region Login
        public IActionResult Login(string returnURL = null)
        {
            LoginVM vm = new LoginVM();
            vm.ReturnURL = returnURL;
            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM vm)
        {
            vm.ReturnURL = vm.ReturnURL ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                //var result = await _signInManager.PasswordSignInAsync(vm.Email, vm.Password, vm.RememberMe, lockoutOnFailure: false);
                var result = await _signInManager.PasswordSignInAsync(vm.Email, vm.Password, vm.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    //return RedirectToAction("Index", "Home");
                    return LocalRedirect(vm.ReturnURL);
                }
                if (result.IsLockedOut)
                {
                    return RedirectToAction(nameof(Lockout));
                }
                else
                {
                    ModelState.AddModelError(string.Empty,"Invlalid login attemp");
                    return View(vm);
                }
            }
            return View(vm);
        }
        public IActionResult Lockout()
        {
            return View();
        }
        #endregion
    }
}