using Microsoft.AspNetCore.Mvc;

namespace RealStateApp.Controllers
{
    public class ClienteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
