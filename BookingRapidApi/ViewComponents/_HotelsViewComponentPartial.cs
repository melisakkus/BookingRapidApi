using BookingRapidApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static BookingRapidApi.Models.FilterHotel;

namespace BookingRapidApi.ViewComponents
{
    public class _HotelsViewComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }

    }
}
