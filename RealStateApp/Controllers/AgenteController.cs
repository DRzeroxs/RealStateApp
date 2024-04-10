using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Interfaces.IServices;
using RealStateApp.Core.Application.ViewModel.AppUsers.Agente;
using System.Runtime.CompilerServices;

namespace RealStateApp.Controllers
{
    public class AgenteController : Controller
    {
        private readonly IAgenteService _agenteService;
        public AgenteController(IAgenteService agenteService)
        {
            _agenteService = agenteService;
        }
        public async Task<IActionResult> Index()
        {
            var agentes = await _agenteService.GetAllAsync();
            return View(agentes);
        }

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
    }
}
