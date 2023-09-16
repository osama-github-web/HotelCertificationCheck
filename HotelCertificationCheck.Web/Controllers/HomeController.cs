using HotelCertificationCheck.Context;
using HotelCertificationCheck.Entities;
using HotelCertificationCheck.Enums;
using HotelCertificationCheck.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly HotelContext _context;
        public HomeController(ILogger<HomeController> logger, UserManager<HotelUser> userManager, RoleManager<IdentityRole> roleManager,  SignInManager<HotelUser> signInManager,HotelContext context)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._logger = logger;
            this._roleManager = roleManager;
            this._context = context;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
            await AddDefaultUsers();
            await AddDefaultHotels();
            return View();
        }

        #region default
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
        
        private async Task AddDefaultHotels()
        {
            var _hotels = new List<Hotel>
            {
                new Hotel
                {
                    BecauseId = 10,
                    Name = "IMLAUER HOTEL PITTER Salzburg\r\n",
                    Country = "Austria",
                    State = "Salzburg",
                    City = "Salzburg",
                    Address = "Rainerstraße",
                    Postalcode = 5020,
                    LatitudeInDecimals = 478077695,
                    LongitudeInDecimals = 130416876,
                    IsCertified = true,
                    CertificationDate = new DateTime(2019,12,11),
                    ExpirationDate  = new DateTime(2023,12,23),
                    Website = "http://www.imlauer.com"
                },
                new Hotel
                {
                    BecauseId = 11,
                    Name = "Hilton Vienna Danube Waterfront",
                    Country = "Austria",
                    State = "Vienna",
                    City = "Wien",
                    Address = "Handelskai 269",
                    Postalcode = 1020,
                    LatitudeInDecimals = 4821408165,
                    LongitudeInDecimals = 1642168449258,
                    IsCertified = true,
                    CertificationDate = new DateTime(2020,12,01),
                    ExpirationDate  = new DateTime(2024,11,03),
                    Website = "http://www.hilton.de/wiendanube"
                },
                new Hotel
                {
                    BecauseId = 12,
                    Name = "Hilton Vienna",
                    Country = "Austria",
                    State = "Vienna",
                    City = "Wien",
                    Address = "Am Stadtpark 1",
                    Postalcode = 1020,
                    LatitudeInDecimals = 4821408165,
                    LongitudeInDecimals = 1642168449258,
                    IsCertified = true,
                    CertificationDate = new DateTime(2020,12,01),
                    ExpirationDate  = new DateTime(2024,11,03),
                    Website = "http://www.hilton.de/wiendanube"
                },
                new Hotel
                {
                    BecauseId = 13,
                    Name = "Steigenberger Hotel Herrenhof",
                    Country = "Austria",
                    State = "Salzburg",
                    City = "Salzburg",
                    Address = "Herrengasse 10",
                    Postalcode = 1010,
                    LatitudeInDecimals = 482097728,
                    LongitudeInDecimals = 163658234,
                    IsCertified = true,
                    CertificationDate = new DateTime(2020,07,27),
                    ExpirationDate  = new DateTime(2026,07,24),
                    Website = "www.herrenhof-wien.steigenberger.at"
                },
                new Hotel
                {
                    BecauseId = 14,
                    Name = "The Harmonie Vienna",
                    Country = "Austria",
                    State = "Vienna",
                    City = "Wien",
                    Address = "Harmoniegasse 5-7",
                    Postalcode = 1090,
                    LatitudeInDecimals = 482199309,
                    LongitudeInDecimals = 163598362,
                    IsCertified = true,
                    CertificationDate = new DateTime(2021,09,04),
                    ExpirationDate  = new DateTime(2025,09,03),
                    Website = "www.harmonie-vienna.at"
                },
                new Hotel
                {
                    BecauseId = 15,
                    Name = "Hotel Am Konzerthaus",
                    Country = "Austria",
                    State = "Vienna",
                    City = "Wien",
                    Address = "Am Heumarkt 35-37",
                    Postalcode = 1030,
                    LatitudeInDecimals = 481996129,
                    LongitudeInDecimals = 163773547,
                    IsCertified = true,
                    CertificationDate = new DateTime(2019,11,25),
                    ExpirationDate  = new DateTime(2023,11,24),
                    Website = "https://sofitel.accorhotels.com/de/hotel-1276-hotel-am-konzerthaus-vienna-mgallery/index.shtml"
                },
                new Hotel
                {
                    BecauseId = 16,
                    Name = "Hotel Bristol",
                    Country = "Austria",
                    State = "Vienna",
                    City = "Wien",
                    Address = "Kärntner Ring 1",
                    Postalcode = 1010,
                    LatitudeInDecimals = 482024907,
                    LongitudeInDecimals = 163702657,
                    IsCertified = true,
                    CertificationDate = new DateTime(2021,11,15),
                    ExpirationDate  = new DateTime(2025,11,14),
                    Website = "http://www.bristolvienna.at"
                },
                new Hotel
                {
                    BecauseId = 17,
                    Name = "Hotel Imperial",
                    Country = "Austria",
                    State = "Vienna",
                    City = "Wien",
                    Address = "Kärntner Ring 16",
                    Postalcode = 1010,
                    LatitudeInDecimals = 482011073,
                    LongitudeInDecimals = 163728733,
                    IsCertified = true,
                    CertificationDate = new DateTime(2021,11,15),
                    ExpirationDate  = new DateTime(2025,11,14),
                    Website = "www.imperialvienna.com"
                },
                new Hotel
                {
                    BecauseId = 18,
                    Name = "WYNDHAM GRAND Salzburg Conference Centre",
                    Country = "Austria",
                    State = "Salzburg",
                    City = "Salzburg",
                    Address = "Fanny-von-Lehnert-Straße",
                    Postalcode = 5020,
                    LatitudeInDecimals = 478153082,
                    LongitudeInDecimals = 130438088,
                    IsCertified = true,
                    CertificationDate = new DateTime(2022,08,30),
                    ExpirationDate  = new DateTime(2026,08,29),
                    Website = "http://www.wyndhamgrandsalzburg.com"
                },
                new Hotel
                {
                    BecauseId = 19,
                    Name = "Hotel Stefanie",
                    Country = "Austria",
                    State = "Vienna",
                    City = "Wien",
                    Address = "Taborstraße 12",
                    Postalcode = 1020,
                    LatitudeInDecimals = 4821439085,
                    LongitudeInDecimals = 16380517955353,
                    IsCertified = true,
                    CertificationDate = new DateTime(2022,05,02),
                    ExpirationDate  = new DateTime(2026,05,01),
                    Website = "www.hotelstefanie.wien"
                },
                new Hotel
                {
                    BecauseId = 20,
                    Name = "InterContinental Wien",
                    Country = "Austria",
                    State = "Vienna",
                    City = "Wien",
                    Address = "Johannesgasse 28",
                    Postalcode = 1030,
                    LatitudeInDecimals = 4820205956729445,
                    LongitudeInDecimals = 16379156112670444,
                    IsCertified = true,
                    CertificationDate = new DateTime(2020,12,17),
                    ExpirationDate  = new DateTime(2024,12,16),
                    Website = "www.vienna.intercontinental.com\r\n"
                },
                new Hotel
                {
                    BecauseId = 21,
                    Name = "Mercure Vienna First",
                    Country = "Austria",
                    State = "Vienna",
                    City = "Wien",
                    Address = "Desider-Friedmann-Platz 2",
                    Postalcode = 1010,
                    LatitudeInDecimals = 482114294,
                    LongitudeInDecimals = 163739598,
                    IsCertified = false,
                    CertificationDate = new DateTime(2022,07,05),
                    ExpirationDate  = new DateTime(2026,07,04),
                    Website = "www.mercure.com/9959"
                },
            };
            foreach (var hotel in _hotels)
            {
                var _isExist = await _context.Hotels.AnyAsync(x => x.BecauseId == hotel.BecauseId);
                if (_isExist)
                    continue;
                try {                 
                    await _context.AddAsync(hotel);
                    await _context.SaveChangesAsync();
                }catch(Exception ex) { }  
            }
        }
        #endregion
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