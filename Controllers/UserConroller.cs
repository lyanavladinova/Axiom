using Data.Entities;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AXIOM.Controllers
{
    public class UserController : Controller
    {
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserDTO user)
        {
            if (string.IsNullOrEmpty(user.Username)
                || string.IsNullOrEmpty(user.Password)
                || string.IsNullOrEmpty(user.ConfirmPassword)) {
                return RedirectToAction("Register");
            }

            if (user.Password != user.ConfirmPassword) {
                return RedirectToAction("Register");
            }

            User newUser = new User();
            newUser.UserName = user.Username;
            newUser.Email = user.Username;
            // TODO Validate
            newUser.FirstName = user.FirstName;
            newUser.LastName = user.LastName;

            //this.context.Users.Add(newUser);
            //this.context.SaveChanges();

            IdentityResult result = await this.userManager.CreateAsync(newUser, user.Password);

            if (!result.Succeeded) {
                return RedirectToAction("Register");
            }

            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserDTO user)
        {
            if (string.IsNullOrEmpty(user.Username)
               || string.IsNullOrEmpty(user.Password)) {
                return RedirectToAction("Login");
            }

            var result = await this.signInManager.PasswordSignInAsync(user.Username, user.Password, false, false);

            if (!result.Succeeded) {
                return RedirectToAction("Login");
            }

            return RedirectToAction("Index", "Article");
        }

        public async Task<IActionResult> Logout()
        {
            await this.signInManager.SignOutAsync();
            return RedirectToAction("Index", "Article");
        }
    }
}
