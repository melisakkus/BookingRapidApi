using Microsoft.AspNetCore.Mvc;

namespace BookingRapidApi.Controllers
{
    public class UIController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetDestinationId()
        {
            return View();
        }
    }
}
