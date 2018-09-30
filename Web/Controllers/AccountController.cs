using Domain.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Words");

            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _authService.AuthenticateAsync(model.UserName, model.Password);
            if (result.State == AuthService.AuthenicationResultState.Fail)
            {
                ModelState.AddModelError(string.Empty, "Incorrect username or password!");
                return View(model);
            }

            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, result.User.UserName),
                new Claim(ClaimTypes.Email, result.User.Email),
                new Claim("FullName", result.User.FullName),
            };

            var Identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(Identity));

            return RedirectToAction("Words", "Admin");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Redirect("/");

        }

        public IActionResult Denied()
        {
            return View();
        }
    }
}