using Microsoft.AspNetCore.Mvc;

namespace RealStateApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
