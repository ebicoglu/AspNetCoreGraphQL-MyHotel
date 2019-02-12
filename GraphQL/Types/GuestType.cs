using GraphQL.Types;
using MyHotel.Entities;

namespace MyHotel.GraphQL.Types
{
    public class GuestType : ObjectGraphType<Guest>
    {
        public GuestType()
        {
            Field(x => x.Id);
            Field(x => x.Name);
            Field(x => x.RegisterDate);
        }
    }
}
