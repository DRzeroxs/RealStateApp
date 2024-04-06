using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Interfaces.IServices;

namespace RealStateApp.Controllers
{
    public class AgenteController : Controller
    {
        private readonly IUserServices _userServices;
        public AgenteController(IUserServices userServices)
        {
            _userServices = userServices;
        }
        public async Task<IActionResult> EliminarAgente(string userId)
        {
            return View("EliminarAgente", userId);    
        }

        [HttpPost]
        public async Task<IActionResult> EliminarAgentePost(string userId)
        {
            await _userServices.EliminarAgente(userId);

            return RedirectToAction("Index", "Admin");
        }
        public async Task<IActionResult> ActivarAgente(string userId)
        {
            await _userServices.ActivarAgente(userId);

            return RedirectToAction("Index", "Admin");
        }
        public async Task<IActionResult> InactivarAgente(string userId)
        {
            await _userServices.InactivarAgente(userId);

            return RedirectToAction("Index", "Admin");
        }
        
    }
}
