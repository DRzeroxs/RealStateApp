using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Interfaces.IServices;
using RealStateApp.Core.Application.ViewModel.Mejora;
using RealStateApp.Core.Application.ViewModel.TipoPropiedad;
using RealStateApp.Core.Application.ViewModel.TipoVenta;

namespace RealStateApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DescripcionController : Controller
    {
        private readonly IMejoraService _mejoraService;
        private readonly ITipoPropiedadService _tipoPropiedadService;
        private readonly ITipoVentaService _tipoVentaService;

        public DescripcionController(IMejoraService mejoraService, ITipoPropiedadService tipoPropiedadService, ITipoVentaService tipoVentaService)
        {
            _mejoraService = mejoraService;
            _tipoPropiedadService = tipoPropiedadService;
            _tipoVentaService = tipoVentaService;
        }

        #region lobby
        [Authorize(Roles = "Admin, Developer")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Developer")]
        public async Task<IActionResult> ListarMejoras()
        {
            List<MejoraViewModel> vm = await _mejoraService.GetAllAsync();

            return View(vm);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Developer")]
        public async Task<IActionResult> ListarTipoDePropiedad()
        {
            List<TipoPropiedadViewModel> vm = await _tipoPropiedadService.GetAllAsync();

            return View(vm);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Developer")]
        public async Task<IActionResult> ListarTipoDeVenta()
        {
            List<TipoVentaViewModel> vm = await _tipoVentaService.GetAllAsync();

            return View(vm);
        }
        #endregion

        #region CRUDs

        #region Mejora

        //Create
        [HttpGet]
        public IActionResult CrearMejora()
        {
            return View( new SaveMejoraViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> CrearMejora(SaveMejoraViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            await _mejoraService.AddAsync(vm);

            return RedirectToAction(nameof(ListarMejoras));
        }

        //Update
        [HttpGet]
        public async Task<IActionResult> EditarMejora(int id)
        {
            MejoraViewModel vm = await _mejoraService.GetByIdAsync(id);

            if (vm == null)
            {
                return RedirectToAction(nameof(ListarMejoras));
            }

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> EditarMejora(SaveMejoraViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            if (vm.Id == 0)
            {
                return RedirectToAction(nameof(ListarMejoras));
            }

            await _mejoraService.UpdateAsync(vm, vm.Id);

            return RedirectToAction(nameof(ListarMejoras));
        }

        //Delete
        [HttpGet]
        public async Task<IActionResult> EliminarMejora(int id)
        {
            MejoraViewModel vm = await _mejoraService.GetByIdAsync(id);

            if (vm == null)
            {
                return RedirectToAction(nameof(ListarMejoras));
            }

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> EliminarMejora(MejoraViewModel vm)
        {
            if (vm.Id == 0)
            {
                return RedirectToAction(nameof(ListarMejoras));
            }

            await _mejoraService.RemoveAsync(vm.Id);

            return RedirectToAction(nameof(ListarMejoras));
        }

        #endregion

        #region TipoPropiedad

        //Create
        [HttpGet]
        public IActionResult CrearTipoPropiedad()
        {
            return View(new SaveTipoPropiedadViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> CrearTipoPropiedad(SaveTipoPropiedadViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            await _tipoPropiedadService.AddAsync(vm);

            return RedirectToAction(nameof(ListarTipoDePropiedad));
        }

        //Update
        [HttpGet]
        public async Task<IActionResult> EditarTipoPropiedad(int id)
        {
            TipoPropiedadViewModel vm = await _tipoPropiedadService.GetByIdAsync(id);

            if (vm == null)
            {
                return RedirectToAction(nameof(ListarTipoDePropiedad));
            }

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> EditarTipoPropiedad(SaveTipoPropiedadViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            if (vm.Id == 0)
            {
                return RedirectToAction(nameof(ListarTipoDePropiedad));
            }

            await _tipoPropiedadService.UpdateAsync(vm, vm.Id);

            return RedirectToAction(nameof(ListarTipoDePropiedad));
        }

        //Delete
        [HttpGet]
        public async Task<IActionResult> EliminarTipoPropiedad(int id)
        {
            TipoPropiedadViewModel vm = await _tipoPropiedadService.GetByIdAsync(id);

            if (vm == null)
            {
                return RedirectToAction(nameof(ListarTipoDePropiedad));
            }

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> EliminarTipoPropiedad(TipoPropiedadViewModel vm)
        {
            if (vm.Id == 0)
            {
                return RedirectToAction(nameof(ListarTipoDePropiedad));
            }

            await _tipoPropiedadService.RemoveAsync(vm.Id);

            return RedirectToAction(nameof(ListarTipoDePropiedad));
        }

        #endregion

        #region TipoVenta

        //Create
        [HttpGet]
        public IActionResult CrearTipoVenta()
        {
            return View(new SaveTipoPropiedadViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> CrearTipoVenta(SaveTipoVentaViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            await _tipoVentaService.AddAsync(vm);

            return RedirectToAction(nameof(ListarTipoDeVenta));
        }

        //Update
        [HttpGet]
        public async Task<IActionResult> EditarTipoVenta(int id)
        {
            TipoVentaViewModel vm = await _tipoVentaService.GetByIdAsync(id);

            if (vm == null)
            {
                return RedirectToAction(nameof(ListarTipoDeVenta));
            }

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> EditarTipoVenta(SaveTipoVentaViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            if (vm.Id == 0)
            {
                return RedirectToAction(nameof(ListarTipoDeVenta));
            }

            await _tipoVentaService.UpdateAsync(vm, vm.Id);

            return RedirectToAction(nameof(ListarTipoDeVenta));
        }

        //Delete
        [HttpGet]
        public async Task<IActionResult> EliminarTipoVenta(int id)
        {
            TipoVentaViewModel vm = await _tipoVentaService.GetByIdAsync(id);

            if (vm == null)
            {
                return RedirectToAction(nameof(ListarTipoDeVenta));
            }

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> EliminarTipoVenta(TipoVentaViewModel vm)
        {
            if (vm.Id == 0)
            {
                return RedirectToAction(nameof(ListarTipoDeVenta));
            }

            await _tipoVentaService.RemoveAsync(vm.Id);

            return RedirectToAction(nameof(ListarTipoDeVenta));
        }

        #endregion

        #endregion

    }
}
