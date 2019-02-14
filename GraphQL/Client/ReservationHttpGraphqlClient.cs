using System.Net.Http;
using System.Threading.Tasks;
using MyHotel.Models;
using Newtonsoft.Json;

namespace MyHotel.GraphQL.Client
{
    public class ReservationHttpGraphqlClient
    {
        private readonly HttpClient _httpClient;

        public ReservationHttpGraphqlClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Response<ReservationContainer>> GetReservationsAsync()
        {
            var response = await _httpClient.GetAsync(
                @"?query={
                reservations {
                checkinDate
                guest  {
                  name
                }
                room {
                  name
                }
              }
            }");

            var stringResult = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Response<ReservationContainer>>(stringResult);
        }
    }
}
