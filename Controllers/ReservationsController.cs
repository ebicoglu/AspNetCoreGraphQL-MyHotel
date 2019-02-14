using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyHotel.GraphQL.Client;
using MyHotel.Models;
using MyHotel.Repositories;

namespace MyHotel.Controllers
{
    [Route("api/[controller]")]
    public class ReservationsController : Controller
    {
        private readonly ReservationRepository _reservationRepository;
        private readonly MyHotelGraphqlClient _myHotelGraphqlClient;

        public ReservationsController(ReservationRepository reservationRepository, MyHotelGraphqlClient myHotelGraphqlClient)
        {
            _reservationRepository = reservationRepository;
            _myHotelGraphqlClient = myHotelGraphqlClient;
        }

        [HttpGet("[action]")]
        public async Task<List<ReservationModel>> List()
        {
            return await _reservationRepository.GetAll<ReservationModel>();
        }

        [HttpGet("[action]")]
        public async Task<List<ReservationModel>> ListFromGraphql()
        {
            var response = await _myHotelGraphqlClient.GetReservationsAsync();
            response.OnErrorThrowException();
            return response.Data.Reservations;
        }

    }
}
