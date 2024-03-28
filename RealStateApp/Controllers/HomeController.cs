using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using RealStateApp.Core.Application.Features.Propiedades.Queries.GetAllPropiedad;
using RealStateApp.Core.Application.Interfaces.IServices;
using RealStateApp.Core.Application.Services;
using RealStateApp.Core.Application.ViewModel.Propiedad;
using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPropiedadService _propiedadesService;
        private readonly IBusquedaPersonalizada _busqueda;
        public HomeController(IPropiedadService propieadadesService, IBusquedaPersonalizada busqueda)
        {
            _propiedadesService = propieadadesService;
            _busqueda = busqueda;
        }
        public async Task<IActionResult> Index()
        {
            var propiedades = await _propiedadesService.GetAllPropiedades();

            return View(propiedades);
        }
        [HttpPost]
        public async Task<IActionResult> BuscarPorCodigo(int identifier)
        {
            var propiedades = await _propiedadesService.GetAllPropiedadesByCode(identifier);

            return View("Index", propiedades);
        }

        [HttpPost]
        public async Task<IActionResult> BusquedaConjunta(string tipoPropiedad,
            int numeroHabitaciones, int numeroAcedados, int precioMinimo, int precioMaximo)
        {
            var propiedades = new List<PropiedadViewModel>();

            propiedades = await _busqueda.BuscarPropiedad(tipoPropiedad,
            numeroHabitaciones,numeroAcedados,precioMinimo,precioMaximo);

            return View("Index", propiedades);

        }
    }
}
