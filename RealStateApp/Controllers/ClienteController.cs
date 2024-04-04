using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Interfaces.IServices;
using RealStateApp.Core.Application.ViewModel.Favorita;
using RealStateApp.Core.Application.ViewModel.Propiedad;

namespace RealStateApp.Controllers
{
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
        public async Task<IActionResult> Favorita(int propiedadId, string userId)
        {
            var cliente = await _clienteService.GetClientePorIdentityId(userId);

            SaveFavoritaViewModel vm = new();
            vm.PropiedadId = propiedadId;
            vm.ClienteId = cliente.Id;

            await _propiedadFavoritaService.AddAsync(vm);
            return View("Index", "Home");
        }
        public async Task<IActionResult> PropiedadesFavoritas(string userId)
        {
           var cliente = await _clienteService.GetClientePorIdentityId(userId);

           var propiedades =  await _propiedadService.GetPropiedadesFavoritas(cliente.Id);

           return View(propiedades);    
        }
    }
}
