using HotelCertificationCheck.Entities;
using HotelCertificationCheck.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelCertificationCheck.Web.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AccountController : Controller
    {
        private readonly UserManager<HotelUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<HotelUser> _signInManager;
        public AccountController(UserManager<HotelUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<HotelUser> signInManager)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task<ActionResult> Index()
        {
            var _users = await _userManager.Users.ToListAsync();
            foreach (var user in _users)
            {
                var _role = await _userManager.GetRolesAsync(user);
                if (_role != null)
                    user.Role = _role.FirstOrDefault();
            }
            return View(_users);
        }        

        [HttpGet("Account/Get/{userId}")]
        public async Task<JsonResult> GetAsync(string userId)
        {
            var _user = await _userManager.FindByIdAsync(userId);
            return Json(_user);
        }

        [HttpPost("Account/Add")]
        public async Task<JsonResult> AddAsync(HotelUser hotelUser)
        {
            if (hotelUser is null)
                return Json("User is Empty");

            if (hotelUser.Password != hotelUser.ConfirmPassword)
                return Json("Password and Confirm Password does not Match");

            var _identityResult = await _userManager.CreateAsync(hotelUser, hotelUser.Password);
            if (!_identityResult.Succeeded)
                return Json($"User not created, {_identityResult.Errors.FirstOrDefault()}");

            if (!await _roleManager.RoleExistsAsync(hotelUser.Role))
                await _roleManager.CreateAsync(new IdentityRole(hotelUser.Role));

            _identityResult = await _userManager.AddToRoleAsync(hotelUser, hotelUser.Role);
            if (!_identityResult.Succeeded)
                return Json($"User not assigned Role, {_identityResult.Errors.FirstOrDefault()}");
            return Json($"User with UserName = {hotelUser?.UserName} Created Successfully.");
        }

        [HttpPost("Account/Delete")]
        public async Task<JsonResult> DeleteAsync(HotelUser hotelUser)
        {
            if (hotelUser is null)
                return Json("User is Empty");

            hotelUser = await _userManager.FindByIdAsync(hotelUser.Id);
            if (hotelUser is null) 
                hotelUser = await _userManager.FindByNameAsync(hotelUser.UserName);
            else if (hotelUser is null) 
                hotelUser = await _userManager.FindByEmailAsync(hotelUser.Email);
            
            try
            {
                var _identityResult = await _userManager.DeleteAsync(hotelUser);
                if (!_identityResult.Succeeded)
                    return Json($"User not Deleted, {_identityResult.Errors.FirstOrDefault()}");
            }
            catch (Exception e) { }
            return Json($"User with UserName ={hotelUser?.UserName} Deleted Successfully.");
        }
    }
}
