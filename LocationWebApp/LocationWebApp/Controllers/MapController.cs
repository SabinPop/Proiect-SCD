using Microsoft.AspNetCore.Mvc;

namespace LocationWebApp.Controllers
{
    public class MapController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
