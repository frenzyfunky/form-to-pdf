using FormToPdf.Admin.Data;
using FormToPdf.Admin.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormToPdf.Admin.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _accountDbContext;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AccountController(ApplicationDbContext accountDbContext, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _accountDbContext = accountDbContext;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(new LoginFormModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HandleLogin(LoginFormModel loginForm)
        {
            var user = await _userManager.FindByNameAsync(loginForm.Username);

            if (user == null)
            {
                return View("Login", loginForm);
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginForm.Password, false);

            if (!result.Succeeded)
            {
                loginForm.IdentityErrors.Add(new IdentityError() { Description = "Invalid username or password" });
                return View("Login", loginForm);
            }

            await _signInManager.SignInAsync(user, false);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HandleRegister(RegisterFormModel registerForm)
        {
            var identityUser = new IdentityUser(registerForm.Email);

            var result = await _userManager.CreateAsync(identityUser, registerForm.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(identityUser, false);
                return RedirectToAction("Index", "Home");
            }


            ViewBag.Errors = result.Errors;
            return View("Register", registerForm);
        }

        [HttpPost]
        public async Task<IActionResult> Logout(RegisterFormModel registerForm)
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
