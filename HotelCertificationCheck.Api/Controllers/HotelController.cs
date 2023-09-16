using HotelCertificationCheck.Entities;
using HotelCertificationCheck.Repository;
using Microsoft.AspNetCore.Mvc;

namespace HotelCertificationCheck.Api.Controllers
{
    public class HotelController : ControllerBase
    {
        private readonly HotelRepository _repository;
        public HotelController(HotelRepository repository)
        {
            this._repository = repository;
        }

        [HttpGet("Hotel/Get")]
        public async Task<ActionResult<List<Hotel>>> GetAsync()
        {
            var _hotels = await _repository.GetAsync();
            return Ok(_hotels);
        }

        [HttpGet("Hotel/Get/{hotelId}")]
        public async Task<ActionResult<List<Hotel>>> GetAsync(string hotelId)
        {
            var _hotel = new Hotel
            {
                Id = Guid.Parse(hotelId)
            };
            _hotel = await _repository.GetAsync(_hotel);
            return Ok(_hotel);
        }

        [HttpPost("Hotel/Add")]
        public async Task<ActionResult<List<Hotel>>> AddAsync(Hotel hotel)
        {
            hotel = await _repository.AddAsync(hotel);
            return Ok(hotel);
        }

        [HttpPost("Hotel/Update")]
        public async Task<ActionResult<List<Hotel>>> UpdateAsync(Hotel hotel)
        {
            hotel = await _repository.UpdateAsync(hotel);
            return Ok(hotel);
        }

        [HttpPost("Hotel/Delete")]
        public async Task<ActionResult<List<Hotel>>> DeleteAsync(Hotel hotel)
        {
            hotel = await _repository.DeleteAsync(hotel);
            return Ok(hotel);
        }
    }
}
