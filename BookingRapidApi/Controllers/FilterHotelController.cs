using BookingRapidApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookingRapidApi.Controllers
{
    public class FilterHotelController : Controller
    {
        public async Task<IActionResult> Index()
        {
            int destinationId = -755070;
            DateTime arrivalDate = new DateTime(2025, 4, 23);
            DateTime depatureDate = new DateTime(2025, 5, 22);
            string arrivalDateStr = arrivalDate.ToString("yyyy-MM-dd");
            string depatureDateStr = depatureDate.ToString("yyyy-MM-dd");

            int adults = 2;
            int room = 1;

            //filtreleyerek otelleri al
            var client2 = new HttpClient();
            var request2 = new HttpRequestMessage
            {
                Method = HttpMethod.Get,

                RequestUri = new Uri($"https://booking-com15.p.rapidapi.com/api/v1/hotels/searchHotels?dest_id={destinationId}&search_type=CITY&arrival_date={arrivalDateStr}&departure_date={depatureDateStr}&adults={adults}&room_qty={room}&page_number=1&units=metric&temperature_unit=c"),


                Headers =
    {
        { "x-rapidapi-key", "e87c6df87cmshcd0612b2e50cc14p1ab93djsnce86952f3d1f" },
        { "x-rapidapi-host", "booking-com15.p.rapidapi.com" },
    },
            };
            using (var response2 = await client2.SendAsync(request2))
            {
                response2.EnsureSuccessStatusCode();
                var jsonBody2 = await response2.Content.ReadAsStringAsync();
                var values2 = JsonConvert.DeserializeObject<FilterHotel>(jsonBody2);
                return View(values2.data);
            }
        }
    }
}
