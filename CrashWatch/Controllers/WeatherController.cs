using Microsoft.AspNetCore.Mvc;

namespace WebApplication3.Controllers
{
    public class WeatherController : Controller
    {
        public IActionResult Index()
        {
            return View("Weather");
        }
    }
}
