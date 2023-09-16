using HotelCertificationCheck.Entities;
using HotelCertificationCheck.Repository;
using Microsoft.AspNetCore.Mvc;

namespace HotelCertificationCheck.Api.Web.Controllers
{
    public class HotelController : Controller
    {
        private readonly HotelRepository _repository;
        public HotelController(HotelRepository repository)
        {
            this._repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var _hotels = await _repository.GetAsync();
            return View(_hotels);
        }

        [HttpGet]
        public async Task<JsonResult> GetAsync(string hotelId)
        {
            var _hotel = new Hotel
            {
                Id = Guid.Parse(hotelId)
            };
            _hotel = await _repository.GetAsync(_hotel);
            return Json(_hotel);
        }

        [HttpPost]
        public async Task<JsonResult> AddAsync(Hotel hotel)
        {
            hotel = await _repository.AddAsync(hotel);
            return Json(hotel);
        }

        [HttpPost]
        public async Task<JsonResult> UpdateAsync(Hotel hotel)
        {
            hotel = await _repository.UpdateAsync(hotel);
            return Json(hotel);
        }

        [HttpPost]
        public async Task<JsonResult> DeleteAsync(Hotel hotel)
        {
            hotel = await _repository.DeleteAsync(hotel);
            return Json(hotel);
        }
    }
}
