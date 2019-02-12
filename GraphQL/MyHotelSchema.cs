using GraphQL;
using GraphQL.Types;

namespace MyHotel.GraphQL
{
    public class MyHotelSchema : Schema
    {
        public MyHotelSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<MyHotelQuery>();
        }
    }
}
