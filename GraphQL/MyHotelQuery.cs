using GraphQL.Types;
using MyHotel.GraphQL.Types;
using MyHotel.Repositories;

namespace MyHotel.GraphQL
{
    public class MyHotelQuery : ObjectGraphType
    {
        /*
         -- Simple test query --

            query TestQuery {
              reservations {
                id
                checkinDate
                checkoutDate
                guest {
                  id
                  name
                  registerDate
                }
                room {
                  id
                  name
                  number
                  allowedSmoking
                  status
                }
              }
            }

        */

        public MyHotelQuery(ReservationRepository reservationRepository)
        {
            Field<ListGraphType<ReservationType>>("reservations",
                resolve: context => reservationRepository.GetAll());
        }
    }
}
