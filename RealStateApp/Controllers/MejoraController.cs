using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Interfaces.IServices;
using RealStateApp.Core.Application.Services;
using RealStateApp.Core.Application.ViewModel.Mejora;
using RealStateApp.Core.Application.ViewModel.TipoVenta;

namespace RealStateApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MejoraController : Controller
    {
        private readonly IMejoraService _mejoraService;
        private readonly IMapper _mapper;
        public MejoraController(IMejoraService mejoraService, IMapper mapper)
        {
            _mejoraService = mejoraService;   
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> ListadoMejoras()
        {
            var mejoras = await _mejoraService.GetAllAsync();   

            return View(mejoras);
        }
        public async Task<IActionResult> CrearMejora()
        {
            SaveMejoraViewModel saveVm = new();
            saveVm.Id = 0;

            return View(saveVm);
        }
        [HttpPost]
        public async Task<IActionResult> CrearMejora(SaveMejoraViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            await _mejoraService.AddAsync(vm);

            return RedirectToAction("ListadoMejoras");
        }
        public async Task<IActionResult> EditarMejora(int Id)
        {
            var tipoPropiedad = await _mejoraService.GetByIdAsync(Id);
            SaveMejoraViewModel saveVm = _mapper.Map<SaveMejoraViewModel>(tipoPropiedad);

            return View("CrearMejora", saveVm);
        }
        [HttpPost]
        public async Task<IActionResult> EditarMejora(SaveMejoraViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            await _mejoraService.UpdateAsync(vm, vm.Id);

            return RedirectToAction("ListadoMejoras");
        }
        public async Task<IActionResult> EliminarMejora(int Id)
        {
            var tipoVenta = await _mejoraService.GetByIdAsync(Id);

            return View(tipoVenta);
        }
        [HttpPost]
        public async Task<IActionResult> EliminarMejoraPost(int Id)
        {
            await _mejoraService.RemoveAsync(Id);

            return RedirectToAction("ListadoMejoras");
        }
    }
}
