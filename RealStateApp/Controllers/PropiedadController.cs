using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Helpers;
using RealStateApp.Core.Application.Interfaces.IServices;
using RealStateApp.Core.Application.ViewModel.Propiedad;

namespace RealStateApp.Controllers
{
    [Authorize(Roles = "Agente, Developer")]
    public class PropiedadController : Controller
    {

        private readonly IPropiedadService _propiedadService;
        private readonly ITipoPropiedadService _tipoPropiedadService;
        private readonly ITipoVentaService _tipoVentaService;
        private readonly IAgenteService _agenteService;

        public PropiedadController(IPropiedadService propiedadService, ITipoPropiedadService tipoPropiedadService, ITipoVentaService tipoVentaService, IAgenteService agenteService)
        {
            _propiedadService = propiedadService;
            _tipoPropiedadService = tipoPropiedadService;
            _tipoVentaService = tipoVentaService;
            _agenteService = agenteService;
        }

        #region lobby
        public async Task<IActionResult> Index()
        {
            List<PropiedadViewModel> vm = await _propiedadService.GetAllAsync();

            return View(vm);
        }

        #endregion

        #region CRUD

        #region Create

        public async Task<IActionResult> Create()
        {
            await CargarViewBags();

            SavePropiedadViewModel savePropiedadViewModel = new();

            return View(savePropiedadViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SavePropiedadViewModel savePropiedadViewModel)
        {
            if (!ModelState.IsValid)
            {
                await CargarViewBags();
                return View(savePropiedadViewModel);
            }

            SavePropiedadViewModel savedVm = await _propiedadService.AddAsync(savePropiedadViewModel);

            if (savedVm == null)
            {
                await CargarViewBags();
                return View(savePropiedadViewModel);
            }

            savedVm.ImgUrls = FileManager.UploadFiles(savePropiedadViewModel.Files, savedVm.Id);
            await _propiedadService.UpdateAsync(savedVm, savedVm.Id);

            return RedirectToAction(nameof(Index));
        }

        #endregion

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return RedirectToAction(nameof(Index));
            }

            PropiedadViewModel vm = await _propiedadService.GetByIdAsync(id);

            return View(vm);
        }

        #endregion

        #region metodos privados
        private async Task CargarViewBags()
        {
            ViewBag.TipoPropiedad = await _tipoPropiedadService.GetAllAsync();
            ViewBag.TipoVenta = await _tipoVentaService.GetAllAsync();
        }
        #endregion
    }
}
