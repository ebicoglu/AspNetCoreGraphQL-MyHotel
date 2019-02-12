using GraphQL.Types;
using MyHotel.Entities;

namespace MyHotel.GraphQL.Types
{
    public class ReservationType : ObjectGraphType<Reservation>
    {
        public ReservationType()
        {
            Field(x => x.Id);
            Field(x => x.CheckinDate).Description("The first day of the stay");
            Field(x => x.CheckoutDate).Description("The leaving day");
            Field<GuestType>(nameof(Reservation.Guest));
            Field<RoomType>(nameof(Reservation.Room));
        }
    }
}
