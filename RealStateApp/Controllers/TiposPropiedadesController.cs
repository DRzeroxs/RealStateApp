using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Interfaces.IServices;
using RealStateApp.Core.Application.ViewModel.TipoPropiedad;

namespace RealStateApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TiposPropiedadesController : Controller
    {
        private readonly ITipoPropiedadService _tiposPropiedadService;
        private readonly IMapper _mapper;
        public TiposPropiedadesController(ITipoPropiedadService tipoPropiedadService, IMapper mapper)
        {
            _tiposPropiedadService = tipoPropiedadService;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> ListadoTipoPropiedades()
        {
            var tipoPropiedadesList = await _tiposPropiedadService.GetTipoPropiedadAsync();

            return View(tipoPropiedadesList);   
        }
        public async Task<IActionResult> CrearTipoPropiedad()
        {
            SaveTipoPropiedadViewModel saveVm = new();
            saveVm.Id = 0;

            return View(saveVm);
        }
        [HttpPost]
        public async Task<IActionResult> CrearTipoPropiedad(SaveTipoPropiedadViewModel vm)
        {
          if(!ModelState.IsValid)
          {
                return View(vm);
          }
         
            await _tiposPropiedadService.AddAsync(vm);

            return RedirectToAction("ListadoTipoPropiedades");
        }
        public async Task<IActionResult> EditarTipoPropiedad(int Id)
        {
            var tipoPropiedad = await _tiposPropiedadService.GetByIdAsync(Id);
            SaveTipoPropiedadViewModel saveVm = _mapper.Map<SaveTipoPropiedadViewModel>(tipoPropiedad);

            return View("CrearTipoPropiedad", saveVm); 
        }
        [HttpPost]
        public async Task<IActionResult> EditarTipoPropiedad(SaveTipoPropiedadViewModel vm)
        {
            if(!ModelState.IsValid)
            {
                return View(vm);  
            }
            await _tiposPropiedadService.UpdateAsync(vm, vm.Id);

            return RedirectToAction("ListadoTipoPropiedades");
        }
        public async Task<IActionResult> EliminarTipoPropiedad(int Id)
        {
            var tipoPropiedad = await _tiposPropiedadService.GetByIdAsync(Id);

            return View(tipoPropiedad);
        }
        [HttpPost]
        public async Task<IActionResult> EliminarTipoPropiedadPost(int Id)
        {
            await _tiposPropiedadService.RemoveAsync(Id);

            return RedirectToAction("ListadoTipoPropiedades");
        }
    }
}
