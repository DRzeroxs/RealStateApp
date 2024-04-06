
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Interfaces.IServices;
using RealStateApp.Core.Application.ViewModel.User;

namespace RealStateApp.Controllers
{
    public class DesarrolladorController : Controller
    {
        private readonly IUserServices _userService;
        private readonly IMapper _mapper;
        public DesarrolladorController(IUserServices userServices, IMapper mapper)
        {
            _userService = userServices;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }
       
        public async Task<IActionResult> CrearDesarrollador()
        {
            UserPostViewModel userPost = new();
            userPost.UserId = "";

            return View(userPost);
        }
        [HttpPost]
        public async Task<IActionResult> CrearDesarrollador(UserPostViewModel vm)
        {
            if(!ModelState.IsValid)
            {
                return View(vm);
            }

            RegistrerViewModel registrerVm = _mapper.Map<RegistrerViewModel>(vm);   

            var response =  await _userService.RegisterDesarrolladorAsync(registrerVm);
            response.userId = "";

            if(response.HasError == true)
            {
                ModelState.AddModelError("Error de Respuesta", $"{response.Error}");
            }

            return RedirectToAction("ListadoDesarrolladores", "Admin");
        }
        public async Task<IActionResult> InactivarDesarrollador(string userId)
        {
            return View("InactivarDesarrollador", userId);
        }
        [HttpPost]
        public async Task<IActionResult> InactivarDesarrolladorPost(string userId)
        {
            await _userService.InactivarDesarrollador(userId);

            return RedirectToAction("ListadoDesarrolladores", "Admin");
        }
        public async Task<IActionResult> ActivarDesarrollador(string userId)
        {
            return View("ActivarDesarrollador", userId);
        }
        [HttpPost]
        public async Task<IActionResult> ActivarDesarrolladorPost(string userId)
        {
            await _userService.ActivarDesarrollador(userId);

            return RedirectToAction("ListadoDesarrolladores", "Admin");
        }
    }
}
