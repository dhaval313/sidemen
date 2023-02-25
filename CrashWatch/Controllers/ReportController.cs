using Microsoft.AspNetCore.Mvc;

namespace WebApplication3.Controllers
{
    public class ReportController : Controller
    {
        public IActionResult Index()
        {
            return View("Report");
        }
    }
}
