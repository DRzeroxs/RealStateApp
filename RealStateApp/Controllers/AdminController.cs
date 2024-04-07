using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Dto.Account;
using RealStateApp.Core.Application.Interfaces.IServices;
using RealStateApp.Core.Application.Services;
using RealStateApp.Core.Application.ViewModel.AppUsers.Agente;
using RealStateApp.Core.Application.ViewModel.User;

namespace RealStateApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly IPropiedadService _propiedadService;
        private readonly IUserServices _userServices;
        private readonly IAgenteService _agenteServices;
        private readonly IMapper _mapper;
        public AdminController(IPropiedadService propiedadService, IUserServices userServices , IAgenteService agenteServices, IMapper mapper)
        {
            _propiedadService = propiedadService;
            _userServices = userServices;
            _agenteServices = agenteServices; 
            _mapper = mapper;

        }
        public async Task <IActionResult> Index()
        {
            await ConteoUsuarios();

            return View();
        }
        public async Task<IActionResult> ListadoAgentes()
        {

            var agentes = await _agenteServices.GetAgenteConPropiedades();

            return View(agentes);
        }
        public async Task<IActionResult> ListadoAdministradores()
        {
            var administradores = await _userServices.GetUsuariosAdministradores(); 

            return View(administradores);   
        }
        public async Task<IActionResult> ListadoDesarrolladores()
        {
            var desarrolladores = await _userServices.GetUsuariosDesarrolladores();

            return View(desarrolladores);
        }
      
        public async Task<IActionResult> CrearAdministrador()
        {
            UserPostViewModel userPost = new();
            userPost.UserId = "";

            return View(userPost);
        }
        [HttpPost]
        public async Task<IActionResult> CrearAdministrador(UserPostViewModel vm)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            var origin = Request.Headers["origin"];

            RegistrerViewModel registrerVm = _mapper.Map<RegistrerViewModel>(vm);
            registrerVm.TypeOfUser = "Admin";

            RegistrerResponse response = await _userServices.RegisterAdminAsync(registrerVm, origin);
          
            return View();  
        }
        public async Task<IActionResult> ActivarAdmin(string userId)
        {
            return View("ActivarAdmin", userId);
        }
        [HttpPost]
        public async Task<IActionResult> ActivarAdminPost(string userId)
        {
            await _userServices.ActivarUsuario(userId);
            
            return RedirectToAction("ListadoAdministradores");
        }
        public async Task<IActionResult> InactivarAdmin(string userId)
        {
            return View("InactivarAdmin", userId);
        }
        [HttpPost]
        public async Task<IActionResult> InactivarAdminPost(string userId)
        {
            await _userServices.InactivarUsuario(userId);

            return RedirectToAction("ListadoAdministradores");
        }

        public async Task<IActionResult> EditarAdmin(string userId)
        {
            var user = await _userServices.GetUserById(userId); 

            return View("CrearAdministrador", user);
        }
        [HttpPost]
        public async Task<IActionResult> EditarAdminPost(UserPostViewModel vm)
        {
            await _userServices.EditarUsuario(vm);

            return RedirectToAction("ListadoAdministradores");
        }
        private async Task ConteoUsuarios()
        {
            ViewBag.contPropiedades = await _propiedadService.ContarPropieades();
            ViewBag.countAgentesActivos = await _userServices.ContarAgentesActivos();
            ViewBag.countAgentesInactivos = await _userServices.ContarAgentesInactivos();
            ViewBag.countClientesActivos = await _userServices.ContarClientesActivos();
            ViewBag.countClientesInactivos = await _userServices.ContarClientesInactivos();
            ViewBag.countDesarrolladoresActivos = await _userServices.ContarDesarrolladoresActivos();
            ViewBag.countDesarrolladoresInactivos = await _userServices.ContarDesarrolladoresInactivos();
        }
    }
}
