using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Interfaces.IServices;
using RealStateApp.Core.Application.Services;
using RealStateApp.Core.Application.ViewModel.TipoPropiedad;
using RealStateApp.Core.Application.ViewModel.TipoVenta;

namespace RealStateApp.Controllers
{
    public class TipoVentasController : Controller
    {
        private readonly ITipoVentaService _tipoVentaService;
        private readonly IMapper _mapper;
        public TipoVentasController(ITipoVentaService tipoVentaService, IMapper mapper)
        {
            _tipoVentaService = tipoVentaService;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> ListadoTipoVentas()
        {
            var tipoVentas = await _tipoVentaService.GetTipoVentasAsync();  

            return View(tipoVentas);
        }
        public async Task<IActionResult> CrearTipoVenta()
        {
            SaveTipoVentaViewModel saveVm = new();
            saveVm.Id = 0;

            return View(saveVm);
        }
        [HttpPost]
        public async Task<IActionResult> CrearTipoVenta(SaveTipoVentaViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            await _tipoVentaService.AddAsync(vm);

            return RedirectToAction("ListadoTipoVentas");
        }
        public async Task<IActionResult> EditarTipoVenta(int Id)
        {
            var tipoPropiedad = await _tipoVentaService.GetByIdAsync(Id);
            SaveTipoVentaViewModel saveVm = _mapper.Map<SaveTipoVentaViewModel>(tipoPropiedad);

            return View("CrearTipoVenta", saveVm);
        }
        [HttpPost]
        public async Task<IActionResult> EditarTipoVenta(SaveTipoVentaViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            await _tipoVentaService.UpdateAsync(vm, vm.Id);

            return RedirectToAction("ListadoTipoVentas");
        }
        public async Task<IActionResult> EliminarTipoVenta(int Id)
        {
            var tipoVenta = await _tipoVentaService.GetByIdAsync(Id);

            return View(tipoVenta);
        }
        [HttpPost]
        public async Task<IActionResult> EliminarTipoVentaPost(int Id)
        {
            await _tipoVentaService.RemoveAsync(Id);

            return RedirectToAction("ListadoTipoVentas");
        }
    }
}
