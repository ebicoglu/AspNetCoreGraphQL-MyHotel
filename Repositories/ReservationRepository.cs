using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MyHotel.EntityFrameworkCore;

namespace MyHotel.Repositories
{
    public class ReservationRepository
    {
        private readonly MyHotelDbContext _myHotelDbContext;

        public ReservationRepository(MyHotelDbContext myHotelDbContext)
        {
            _myHotelDbContext = myHotelDbContext;
        }

        public async Task<List<T>> GetAll<T>()
        {
            return await _myHotelDbContext
                .Reservations
                .Include(x => x.Room)
                .Include(x => x.Guest)
                .ProjectTo<T>()
                .ToListAsync();
        }
    }
}
