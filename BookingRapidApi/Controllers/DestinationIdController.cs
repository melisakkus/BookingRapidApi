using BookingRapidApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookingRapidApi.Controllers
{
    public class DestinationIdController : Controller
    {
        public async Task<IActionResult> Index(string cityName)
        {

            cityName = "Ankara";
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
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var jsonBody = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<Destination>(jsonBody);
                ViewBag.destinationId = values.data.Where(x => x.country == "Turkey").Select(x=>x.dest_id).FirstOrDefault();
                return View(values.data);
            }
        }
    }
}