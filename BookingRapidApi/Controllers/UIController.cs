using BookingRapidApi.Models;
using BookingRapidApi.ViewComponents;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
    }
}
