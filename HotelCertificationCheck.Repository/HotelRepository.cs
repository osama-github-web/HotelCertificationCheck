using HotelCertificationCheck.Context;
using HotelCertificationCheck.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelCertificationCheck.Repository
{
    public class HotelRepository : IRepository<Hotel>
    {
        private readonly HotelContext _context;
        public HotelRepository(HotelContext context) => this._context = context;

        public async Task<List<Hotel>> GetAsync()
        {
            return await _context.Hotels.ToListAsync();
        }

        public async Task<Hotel> GetAsync(Hotel entity)
        {
            return await _context.Hotels.FindAsync(entity.Id);
        }
        
        public async Task<Hotel> GetAsync(int becauseId)
        {
            return await _context.Hotels.FirstOrDefaultAsync(x => x.BecauseId == becauseId);
        }

        public async Task<Hotel> AddAsync(Hotel entity)
        {
            try
            {
                await _context.Hotels.AddAsync(entity);
                if(await _context.SaveChangesAsync() >0)
                    return entity;
            }catch (Exception ex)
            { }
            return null;
        }

        public async Task<Hotel> DeleteAsync(Hotel entity)
        {
            try
            {
                var _hotel = await GetAsync(entity);
                if (_hotel is null)
                    return null;

                _context.Hotels.Remove(_hotel);
                if (await _context.SaveChangesAsync() > 0)
                    return entity;
            }
            catch (Exception ex)
            { }
            return null;
        }

        public async Task<Hotel> UpdateAsync(Hotel entity)
        {
            try
            {
                var _hotel = await GetAsync(entity);
                if (_hotel is null)
                    return null;

                _hotel = _hotel.Update(entity);

                _context.Hotels.Update(_hotel);
                if (await _context.SaveChangesAsync() > 0)
                    return entity;
            }
            catch (Exception ex)
            { }
            return null;
        }
    }
}
