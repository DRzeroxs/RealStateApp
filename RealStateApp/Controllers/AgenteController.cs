using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Interfaces.IServices;
using RealStateApp.Core.Application.ViewModel.AppUsers.Agente;
using System.Runtime.CompilerServices;

namespace RealStateApp.Controllers
{
    //[Authorize(Roles = "Agente")]
    public class AgenteController : Controller
    {
        private readonly IAgenteService _agenteService;
        private readonly IUserServices _userServices;
        public AgenteController(IAgenteService agenteService, IUserServices userServices)
        {
            _agenteService = agenteService;
            _userServices = userServices;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var agentes = await _agenteService.GetAllAsync();
            return View(agentes);
        }

        [AllowAnonymous]
        public async Task<IActionResult> AgenteByName(string nombre)
        {
            var agente = await _agenteService.GetAgenteByNombre(nombre);
            if (agente.HasError)
            {
                agente.Error = "No se encontro un agente con ese nombre";
            }
            List<AgenteViewModel> agenteViewModels = new List<AgenteViewModel> {agente};
            return View("Index", agenteViewModels);
        }
        
        public async Task<IActionResult> EliminarAgente(string userId)
        {
            return View("EliminarAgente", userId);    
        }

        [HttpPost]
        public async Task<IActionResult> EliminarAgentePost(string userId)
        {
            await _userServices.EliminarAgente(userId);

            return RedirectToAction("ListadoAgentes", "Admin");
        }
        public async Task<IActionResult> ActivarAgente(string userId)
        {

           return View("ActivarAgente",userId);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> ActivarAgentePost(string userId)
        {
            await _userServices.ActivarUsuario(userId);

            return RedirectToAction("ListadoAgentes", "Admin");
        }
        public async Task<IActionResult> InactivarAgente(string userId)
        {
            return View("InactivarAgente", userId);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> InactivarAgentePost(string userId)
        {
            await _userServices.InactivarUsuario(userId);

            return RedirectToAction("ListadoAgentes", "Admin");
        }
        
    }
}
