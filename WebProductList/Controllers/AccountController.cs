using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebProductList.Models;

namespace WebProductList.Controllers
{
    public class AccountController : Controller {
        private readonly IEnumerable<User> _users;
        public AccountController(IOptions<List<User>> options) {
            _users = options.Value;
        }

        [HttpGet]
        public IActionResult Login() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user) {
            if (ModelState.IsValid) {
                if (_users.FirstOrDefault(u => u.Equals(user)) != default(User)) {
                    var claims = new List<Claim> {
                        new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name)
                    };
                    ClaimsIdentity identuty = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identuty));
                    return RedirectToAction("Index", "ProductList");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(user);
        }

        public async Task<IActionResult> Logout() {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}