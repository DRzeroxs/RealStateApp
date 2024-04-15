using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Interfaces.IServices;
using RealStateApp.Core.Application.ViewModel.Favorita;
using RealStateApp.Core.Application.ViewModel.Propiedad;

namespace RealStateApp.Controllers
{
    [Authorize(Roles = "Cliente")]
    public class ClienteController : Controller
    {
        private readonly IUserServices _userServices;
        private readonly IClienteService _clienteService;   
        private readonly IPropiedadFavoritaService _propiedadFavoritaService;
        private readonly IPropiedadService _propiedadService;

        public ClienteController(IUserServices userServices, IPropiedadFavoritaService propiedadFavoritaService, IClienteService clienteService, IPropiedadService propiedadService)
        {
            _userServices = userServices;
            _propiedadFavoritaService = propiedadFavoritaService;   
            _clienteService = clienteService;
            _propiedadService = propiedadService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Favorita(int propiedadId, string userId)
        {
            var cliente = await _clienteService.GetClientePorIdentityId(userId);

            SaveFavoritaViewModel vm = new();
            vm.PropiedadId = propiedadId;
            vm.ClienteId = cliente.Id;

            var favorita = await _propiedadFavoritaService.GetPropiedadFavoritaPorPropiedadId(propiedadId);

            if(favorita != null)
            {
                ModelState.AddModelError("Se encuentra agregada", "Esta Propiedad ya esta marcada como Favorita");

                return RedirectToAction("PropiedadesFavoritas", new {userId = userId });
            }

            await _propiedadFavoritaService.AddAsync(vm);

            return RedirectToAction("PropiedadesFavoritas", new { userId = userId });
        }
        public async Task<IActionResult> PropiedadesFavoritas(string userId)
        {
           var cliente = await _clienteService.GetClientePorIdentityId(userId);

           var propiedades =  await _propiedadService.GetPropiedadesFavoritas(cliente.Id);

           return View(propiedades);    
        }
        [HttpPost]
        public async Task<IActionResult> EliminarPropiedadFavorita(int propiedadId, string userId)
        {
            var cliente = await _clienteService.GetClientePorIdentityId(userId);

            SaveFavoritaViewModel vm = new();
            vm.PropiedadId = propiedadId;
            vm.ClienteId = cliente.Id;

            var favorita = await _propiedadFavoritaService.GetPropiedadFavoritaPorPropiedadId(propiedadId);

            await _propiedadFavoritaService.RemoveAsync(favorita.Id);

            return RedirectToAction("PropiedadesFavoritas", new { userId = userId });
        }
    }
}
