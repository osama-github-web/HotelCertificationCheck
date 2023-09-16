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
        public HomeController(ILogger<HomeController> logger, UserManager<HotelUser> userManager,  SignInManager<HotelUser> signInManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._logger = logger;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
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