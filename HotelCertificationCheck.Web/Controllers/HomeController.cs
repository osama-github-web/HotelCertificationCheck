using HotelCertificationCheck.Entities;
using HotelCertificationCheck.Enums;
using HotelCertificationCheck.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HotelCertificationCheck.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<HotelUser> _userManager;
        private readonly SignInManager<HotelUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public HomeController(ILogger<HomeController> logger, UserManager<HotelUser> userManager, RoleManager<IdentityRole> roleManager,  SignInManager<HotelUser> signInManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._logger = logger;
            this._roleManager = roleManager;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
            await AddDefaultUsers();
            return View();
        }

        private async Task AddDefaultUsers()
        {
            var _users = new List<HotelUser>
            {
                new HotelUser
                {
                    UserName = "admin",
                    Email = "admin@gmail.com",
                    Password = "admin",
                    Role = ERoles.Admin.ToString()
                },
                new HotelUser
                {
                    UserName = "user",
                    Email = "user@gmail.com",
                    Password = "user",
                    Role = ERoles.User.ToString()
                }
            };
            foreach (var user in _users)
            {
                if (await _userManager.FindByNameAsync(user.UserName) is not null)
                    continue;

                var _identityResult = await _userManager.CreateAsync(user, user.Password);
                if (_identityResult.Succeeded)
                {
                    if (!await _roleManager.RoleExistsAsync(user.Role))
                        await _roleManager.CreateAsync(new IdentityRole(user.Role));
                    await _userManager.AddToRoleAsync(user, user.Role);
                }
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(HotelUser hotelUser)
        {
            if (string.IsNullOrWhiteSpace(hotelUser.UserName))
                return View(hotelUser);

            var _user = await _userManager.FindByNameAsync(hotelUser.UserName);
            if(_user is null)
                return View(hotelUser);

            var _signInResult = await _signInManager.PasswordSignInAsync(_user.UserName, hotelUser.Password, hotelUser.IsPersistent, false);
            if (!_signInResult.Succeeded)
                return View(hotelUser);

            var _role = await _userManager.GetRolesAsync(_user);
            if (_role.FirstOrDefault() == ERoles.Admin.ToString())
                return RedirectToAction("Index", "Account");
            return RedirectToAction("Index", "Client");
        }

        public async Task<IActionResult> Logout()
        {
           await _signInManager.SignOutAsync();
            return RedirectToAction("Login","Home");
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}