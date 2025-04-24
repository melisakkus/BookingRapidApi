using BookingRapidApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Drawing;

namespace BookingRapidApi.ViewComponents
{
    public class _SliderViewComponentPartial : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }  
    }
}

