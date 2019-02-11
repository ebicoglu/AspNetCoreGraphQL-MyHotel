using System.Collections.Generic;
using System.Linq;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyHotel.EntityFrameworkCore;
using MyHotel.Models;

namespace MyHotel.Controllers
{
    [Route("api/[controller]")]
    public class ReservationsController : Controller
    {
        private readonly MyHotelDbContext _myHotelDbContext;

        public ReservationsController(MyHotelDbContext myHotelDbContext)
        {
            _myHotelDbContext = myHotelDbContext;
        }

        [HttpGet("[action]")]
        public List<ReservationModel> List()
        {
            return _myHotelDbContext
                .Reservations
                .Include(x => x.Room)
                .Include(x => x.Guest)
                .ProjectTo<ReservationModel>()
                .ToList();
        }
    }
}
