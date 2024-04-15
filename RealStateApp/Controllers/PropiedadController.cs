using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using RealStateApp.Core.Application.Dto.Account;
using RealStateApp.Core.Application.Helpers;
using RealStateApp.Core.Application.Interfaces.IServices;
using RealStateApp.Core.Application.ViewModel.AppUsers.Agente;
using RealStateApp.Core.Application.ViewModel.Propiedad;
using System.Numerics;

namespace RealStateApp.Controllers
{
    //[Authorize(Roles = "Agente")]
    public class PropiedadController : Controller
    {

        private readonly IPropiedadService _propiedadService;
        private readonly ITipoPropiedadService _tipoPropiedadService;
        private readonly ITipoVentaService _tipoVentaService;
        private readonly IMejoraService _mejoraService;
        private readonly IAgenteService _agenteService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse userVm;

        public PropiedadController(IPropiedadService propiedadService, ITipoPropiedadService tipoPropiedadService, ITipoVentaService tipoVentaService, IAgenteService agenteService, IMejoraService mejoraService, IHttpContextAccessor httpContextAccessor)
        {
            _propiedadService = propiedadService;
            _tipoPropiedadService = tipoPropiedadService;
            _tipoVentaService = tipoVentaService;
            _agenteService = agenteService;
            _mejoraService = mejoraService;
            _httpContextAccessor = httpContextAccessor;
            userVm = httpContextAccessor.HttpContext.Session.get<AuthenticationResponse>("User");
        }

        #region lobby
        [AllowAnonymous]
        public async Task<IActionResult> Index(string userId)
        {
            //List<PropiedadViewModel> vm = await _propiedadService.GetAllPropiedades();
            var agente = await _agenteService.GetByIdentityId(userId);

            var propiedades = await _propiedadService.GetPropiedadesDelAgente(agente.Id);

            return View(propiedades);
        }

        #endregion

        #region CRUD

        #region Crear

        public async Task<IActionResult> CrearPropiedad()
        {
            await CargarViewBags();

            SavePropiedadViewModel savePropiedadViewModel = new();

            return View(savePropiedadViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearPropiedad(SavePropiedadViewModel savePropiedadViewModel, string userId)
        {
            if (!ModelState.IsValid)
            {
                await CargarViewBags();
                return View(savePropiedadViewModel);
            }

            savePropiedadViewModel.Identifier = await _propiedadService.GenerarIdentificadorUnico();

            AgenteViewModel agente = await _agenteService.GetByIdentityId(userId);

            if (agente == null)
            {
                await CargarViewBags();
                // Agregar mensaje de error en la vista "No se encontro al agente"
                return View(savePropiedadViewModel);
            }

            savePropiedadViewModel.AgenteId = agente.Id;



            SavePropiedadViewModel savedVm = await _propiedadService.AddAsync(savePropiedadViewModel);

            if (savedVm == null)
            {
                await CargarViewBags();
                return View(savePropiedadViewModel);
            }

            savedVm.ImgUrls = FileManager.UploadFiles(savePropiedadViewModel.Files, savedVm.Id);

            return RedirectToRoute(new { controller = "Propiedad", action = "Index", userId = $"{userId}" });
        }

        #endregion

        #region Editar

        [HttpGet]
        public async Task<IActionResult> EditarPropiedad(int id)
        {
            if (id == 0)
            {
                return RedirectToAction(nameof(Index));
            }

            PropiedadViewModel vm = await _propiedadService.GetByIdAsync(id);

            if (vm == null)
            {
                return RedirectToAction(nameof(Index));
            }

            SavePropiedadViewModel savePropiedadViewModel = new()
            {
                Id = vm.Id,
                Identifier = vm.Identifier,
                Size = vm.Size,
                Precio = vm.Precio,
                Descripcion = vm.Descripcion,
                NumHabitaciones = vm.NumHabitaciones,
                NumAceados = vm.NumAceados,
                TipoPropiedadId = vm.TipoPropiedadId,
                TipoVentaId = vm.TipoVentaId,
                AgenteId = vm.AgenteId,
                ImgUrls = AgregarImagen(vm.ImgUrlList)
            };

            await CargarViewBags();

            return View("CrearPropiedad", savePropiedadViewModel);
        }
        private List<string> AgregarImagen(List<ImgPropiedadViewModel> imgagenes)
        {
            List< string> result = new List< string>(); 

            foreach(var img in imgagenes)
            {
                result.Add(img.UrlImg);
            }

            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarPropiedad(SavePropiedadViewModel savePropiedadViewModel)
        {
            if (!ModelState.IsValid)
            {
                await CargarViewBags();
                return View("CrearPropiedad", savePropiedadViewModel);
            }

            PropiedadViewModel vm = await _propiedadService.GetByIdAsync(savePropiedadViewModel.Id);

            if (vm == null)
            {
                return RedirectToAction(nameof(Index));
            }

            List<string> urlsImgPropiedades = new();

            foreach (var item in vm.ImgUrlList)
            {
                urlsImgPropiedades.Add(item.UrlImg);
            }

            savePropiedadViewModel.ImgUrls = FileManager.UploadFiles(savePropiedadViewModel.Files, savePropiedadViewModel.Id, true,urlsImgPropiedades);

            await _propiedadService.UpdateAsync(savePropiedadViewModel, savePropiedadViewModel.Id);

            return RedirectToRoute(new { controller = "Propiedad", action = "Index", userId = $"{userVm.Id}" });
        }

        #endregion

        #region Eliminar

        [HttpGet]
        public async Task<IActionResult> Eliminar(int id)
        {
            PropiedadViewModel vm = await _propiedadService.GetByIdAsync(id);

            if (vm == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(vm);
        }

        [HttpPost, ActionName("Eliminar")]
       // [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarPropiedad(int id)
        {
            if (id == 0)
            {
                return RedirectToAction(nameof(Index));
            }

            await _propiedadService.RemoveAsync(id);

            return RedirectToRoute(new { controller = "Propiedad", action = "Index", userId = $"{userVm.Id}" });
        }

        #endregion

        #endregion

        #region metodos privados
        private async Task CargarViewBags()
        {
            ViewBag.TipoPropiedad = await _tipoPropiedadService.GetAllAsync();
            ViewBag.TipoVenta = await _tipoVentaService.GetAllAsync();
            ViewBag.Mejoras = await _mejoraService.GetAllAsync();
        }
        #endregion

        #region "Propiedades del Agente"
        public async Task<IActionResult> PropiedadesDelAgente()
        {
            //var agente = await _agenteService.GetByIdentityId(userId);

            //var propiedades = await _propiedadService.GetPropiedadesDelAgente(agente.Id);

            List<PropiedadViewModel> vm = await _propiedadService.GetAllPropiedades();

            return View(vm);
        }
        #endregion
    }
}
