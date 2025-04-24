using BookingRapidApi.Models;
using BookingRapidApi.ViewComponents;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static BookingRapidApi.Models.FilterHotel;

namespace BookingRapidApi.Controllers
{
    [Route("[controller]")]
    public class UIController : Controller
    {
        [HttpGet("Index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("GetId/{cityName}")]
        public async Task<IActionResult> GetId(string cityName)
        {
            int destinationId;
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://booking-com15.p.rapidapi.com/api/v1/hotels/searchDestination?query={cityName}"),
                Headers =
            {
                { "x-rapidapi-key", "e87c6df87cmshcd0612b2e50cc14p1ab93djsnce86952f3d1f" },
                { "x-rapidapi-host", "booking-com15.p.rapidapi.com" },
            },
            };

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var jsonBody = await response.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Destination>(jsonBody);
            string destination = values.data?.Where(x => x.country == "Turkey").Select(x => x.dest_id).FirstOrDefault();
            if (!string.IsNullOrEmpty(destination))
            {
                destinationId = Convert.ToInt32(destination);
                return Ok(destinationId);
            }
            return NotFound("Belirtilen şehir için Türkiye'de sonuç bulunamadı.");
        }

        [HttpGet("GetFilterHotels/{destid}/{arrivalDate}/{departureDate}/{adults}/{room}")]
        public async Task<IActionResult> GetFilterHotels(string destid, DateTime arrivalDate, DateTime departureDate, int adults, int room)
        {
            string arrivalDateStr = arrivalDate.ToString("yyyy-MM-dd");
            string depatureDateStr = departureDate.ToString("yyyy-MM-dd");

            var client2 = new HttpClient();
            var request2 = new HttpRequestMessage
            {
                Method = HttpMethod.Get,

                RequestUri = new Uri($"https://booking-com15.p.rapidapi.com/api/v1/hotels/searchHotels?dest_id={destid}&search_type=CITY&arrival_date={arrivalDateStr}&departure_date={depatureDateStr}&adults={adults}&room_qty={room}&page_number=1&units=metric&temperature_unit=c"),

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
                return Ok(values2?.data.hotels);

            }
        }
    }
}
