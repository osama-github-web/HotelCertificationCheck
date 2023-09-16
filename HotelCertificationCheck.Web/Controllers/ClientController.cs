using HotelCertificationCheck.Entities;
using HotelCertificationCheck.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelCertificationCheck.Web.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class ClientController : Controller
    {
        private readonly HotelRepository _repository;
        public ClientController(HotelRepository repository)
        {
            this._repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var _hotels = await _repository.GetAsync();
            return View(_hotels);
        }

        [HttpGet("Client/VerifyHotelCertification/{becauseId}")]
        public async Task<JsonResult> VerifyHotelCertification(int becauseId)
        {
            var _hotel = await _repository.GetAsync(becauseId);
            if(_hotel == null || _hotel.IsCertified == false) 
                return Json("Hotel is not Certified");
            return Json("Hotel is Certified");
        }
    }
}
