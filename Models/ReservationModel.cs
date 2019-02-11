using System;

namespace MyHotel.Models
{
    public class ReservationModel
    {
        public int Id { get; set; }

        public RoomModel Room { get; set; }

        public GuestModel Guest { get; set; }

        public DateTime CheckinDate { get; set; }

        public DateTime CheckoutDate { get; set; }
    }
}
