using HotelCertificationCheck.Entities;
using HotelCertificationCheck.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelCertificationCheck.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HotelController : Controller
    {
        private readonly HotelRepository _repository;
        public HotelController(HotelRepository repository)
        {
            this._repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var _hotels = await _repository.GetAsync();
            return View(_hotels);
        }
        
        public async Task<IActionResult> Client()
        {
            var _hotels = await _repository.GetAsync();
            return View(_hotels);
        }

        [HttpGet("Hotel/Get")]
        public async Task<JsonResult> GetAsync(string hotelId)
        {
            var _hotel = new Hotel
            {
                Id = Guid.Parse(hotelId)
            };
            _hotel = await _repository.GetAsync(_hotel);
            return Json(_hotel);
        }

        [HttpPost("Hotel/Add")]
        public async Task<JsonResult> AddAsync(Hotel hotel)
        {
            hotel = await _repository.AddAsync(hotel);
            if(hotel is null)
                return Json("Hotel not Added");
            return Json("Hotel Added Successfully");
        }

        [HttpPost("Hotel/Update")]
        public async Task<JsonResult> UpdateAsync(Hotel hotel)
        {
            hotel = await _repository.UpdateAsync(hotel);
            if (hotel is null)
                return Json("Hotel not Updated");
            return Json("Hotel Updated Successfully");
        }

        [HttpPost("Hotel/Delete")]
        public async Task<JsonResult> DeleteAsync(Hotel hotel)
        {
            hotel = await _repository.DeleteAsync(hotel);
            if (hotel is null)
                return Json("Hotel not Deleted");
            return Json("Hotel Deleted Successfully");
        } 
    }
}
