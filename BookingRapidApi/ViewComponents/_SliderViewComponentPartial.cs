using BookingRapidApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Drawing;

namespace BookingRapidApi.ViewComponents
{
    public class _SliderViewComponentPartial : ViewComponent
    {

        //public IViewComponentResult Invoke()
        //{
        //    return View();
        //}


        public async Task<IViewComponentResult> InvokeAsync(string cityName, DateTime arrivalDate, DateTime depatureDate, int adults, int room)
        {
            //destination id al
            cityName = "Ankara";
            arrivalDate = new DateTime(2025, 4, 23);
            depatureDate = new DateTime(2025, 5, 22);
            adults = 2;
            room = 1;
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
            string destination = values.data.Where(x => x.country == "Turkey").Select(x => x.dest_id).FirstOrDefault();
            destinationId = Convert.ToInt32(destination);
            ViewBag.destinationId = destinationId;

            //filtreleyerek otelleri al
            var client2 = new HttpClient();
            var request2 = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://booking-com15.p.rapidapi.com/api/v1/hotels/searchHotels?dest_id={destinationId}&search_type=CITY&arrival_date={arrivalDate}&departure_date={depatureDate}&adults={adults}&room_qty={room}&page_number=1&units=metric&temperature_unit=c"),
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
