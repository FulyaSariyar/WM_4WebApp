﻿using System.Linq;
using System.Threading.Tasks;
using ItServiceApp.Models;
using ItServiceApp.Models.Identity;
using ItServiceApp.Services;
using ItServiceApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ItServiceApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IEmailSender _emailSender;
        public AccountController(UserManager<ApplicationUser> userManager,
                                SignInManager<ApplicationUser> signInManager,
                                RoleManager<ApplicationRole> roleManager
                                 , IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailSender = emailSender;

            CheckRoles();
           
        }

        private void CheckRoles()
        {
            foreach (var roleName in RoleNames.Roles)
            {
                if (!_roleManager.RoleExistsAsync(roleName).Result) //dikkat!!!!!(result)
                {
                    var result = _roleManager.CreateAsync(new ApplicationRole()
                    {
                        Name = roleName
                    }).Result;
                }
            }

        }



        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Password = string.Empty;
                model.ConfirmPassword = string.Empty;
                return View(model);
            }

            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null)
            {
                ModelState.AddModelError(nameof(model.UserName), "Bu kullanıcı adı daha önce sisteme kayıt edilmiştir");
                return View(model);
            }
            user = await _userManager.FindByEmailAsync(model.EmailAddress);
            if (user != null)
            {
                ModelState.AddModelError(nameof(model.EmailAddress), "Bu email daha önce sisteme kayıt edilmiştir");
                return View(model);
            }

            user = new ApplicationUser()
            {
                UserName = model.UserName,
                Email = model.EmailAddress,
                Name = model.Name,
                Surname = model.SurName
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                //TODO:kullanıcıya rol atama
                var count = _userManager.Users.Count();
                if (count == 1) //admin
                {
                    result = await _userManager.AddToRoleAsync(user, RoleNames.Admin);
                }
                else//user 
                {
                    result = await _userManager.AddToRoleAsync(user, RoleNames.User);
                }

                //TODO:kullanıcıya email doğrulama gönderme
                //TODO:giriş sayfasına yönlendirme
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Kayıt işleminde bir hata oluştu");
                return View(model);
            }


            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
           
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, true);
            if (result.Succeeded)
            {
                
                    await _emailSender.SendAsync(new EmailMessage()
                    {
                        Contacts = new string[] { "fulyasariyar@outlook.com" },
                        Body = $"{HttpContext.User.Identity.Name} Sisteme giriş yaptı!",
                        Subject = $"Merhaba {HttpContext.User.Identity.Name}"
                    });
                    return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Kullanıcı adı veya şifre hatalı!");
                return View(model);
            }

        }
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}