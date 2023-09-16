using HotelCertificationCheck.Entities;
using HotelCertificationCheck.Enums;
using HotelCertificationCheck.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace HotelCertificationCheck.Api.Controllers
{
    public class AccountController : ControllerBase
    {
        private readonly UserManager<HotelUser> _userManager;
        private readonly RoleManager<IdentityRole>_roleManager;
        public AccountController(UserManager<HotelUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        [HttpGet("Account/Get")]
        public async Task<ActionResult<List<HotelUser>>> GetAsync()
        {
            var _users = await _userManager.Users.ToListAsync();
            return Ok(_users);
        }
        
        [HttpGet("Account/Get/{userId}")]
        public async Task<ActionResult<HotelUser>> GetAsync(string userId)
        {
            var _user = await _userManager.FindByIdAsync(userId);
            return Ok(_user);
        }
        
        [HttpPost("Account/Add")]
        public async Task<ActionResult<HotelUser>> AddAsync(HotelUser hotelUser)
        {
            if (hotelUser is null)
                return BadRequest("User is Empty");

            if(hotelUser.Password != hotelUser.ConfirmPassword)
                return BadRequest("Password and Confirm Password does not Match");

            var _identityResult = await _userManager.CreateAsync(hotelUser,hotelUser.Password);
            if (!_identityResult.Succeeded)
                return BadRequest($"User not created, {_identityResult.Errors.FirstOrDefault()}");

            if (!await _roleManager.RoleExistsAsync(ERoles.User.ToString()))
                await _roleManager.CreateAsync(new IdentityRole(ERoles.User.ToString()));

            _identityResult = await _userManager.AddToRoleAsync(hotelUser, ERoles.User.ToString());
            if (!_identityResult.Succeeded)
                return BadRequest($"User not assigned Role, {_identityResult.Errors.FirstOrDefault()}");

            return Ok("User Created Successfully");
        }       
        
        
        [HttpPost("Account/Delete")]
        public async Task<ActionResult<HotelUser>> DeleteAsync(HotelUser hotelUser)
        {
            if (hotelUser is null)
                return BadRequest("User is Empty");

            var _user = await _userManager.FindByIdAsync(hotelUser.Id);
            if (_user is null) _user = await _userManager.FindByNameAsync(hotelUser.UserName);
            if (_user is null) _user = await _userManager.FindByEmailAsync(hotelUser.Email);

            var _identityResult = await _userManager.DeleteAsync(hotelUser);
            if (!_identityResult.Succeeded)
                return BadRequest($"User not Deleted, {_identityResult.Errors.FirstOrDefault()}");

            return Ok("User Deleted Successfully");
        }
    }
}
