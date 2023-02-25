using Microsoft.AspNetCore.Mvc;

namespace WebApplication3.Controllers
{
    public class MapController : Controller
    {
        public IActionResult Index()
        {
            return View("Map");
        }
    }
}
