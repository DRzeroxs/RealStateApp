using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Interfaces.IServices;

namespace RealStateApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly IPropiedadService _propiedadService;
        private readonly IUserServices _userServices;
        public AdminController(IPropiedadService propiedadService, IUserServices userServices)
        {
            _propiedadService = propiedadService;
            _userServices = userServices;
        }
        public async Task <IActionResult> Index()
        {
            await ConteoUsuarios();

            return View();
        }

        private async Task ConteoUsuarios()
        {
            ViewBag.contPropiedades = await _propiedadService.ContarPropieades();
            ViewBag.countAgentesActivos = await _userServices.ContarAgentesActivos();
            ViewBag.countAgentesInactivos = await _userServices.ContarAgentesInactivos();
            ViewBag.countClientesActivos = await _userServices.ContarClientesActivos();
            ViewBag.countClientesInactivos = await _userServices.ContarClientesInactivos();
            ViewBag.countDesarrolladoresActivos = await _userServices.ContarDesarrolladoresActivos();
            ViewBag.countDesarrolladoresInactivos = await _userServices.ContarDesarrolladoresInactivos();
        }
    }
}
