using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Dto.Account;
using RealStateApp.Core.Application.Interfaces.IServices;
using RealStateApp.Core.Application.Services;
using RealStateApp.Core.Application.ViewModel.User;
using RealStateApp.Core.Application.Helpers;
using RealStateApp.Core.Application.ViewModel.AppUsers.Cliente;
using RealStateApp.Core.Application.ViewModel.AppUsers.Agente;
using Microsoft.AspNetCore.Authorization;

namespace RealStateApp.Controllers
{
    
    public class UserController : Controller
    {
        private readonly IUserServices _userServices;
        private readonly IClienteService _clienteService;
        private readonly IAgenteService _agenteService;
        public UserController(IUserServices userServices, IClienteService clienteService, IAgenteService agenteService)
        {
            _userServices = userServices;
            _clienteService = clienteService;
            _agenteService = agenteService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Registrer()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registrer(RegistrerViewModel vm)
        {
          if(!ModelState.IsValid)
          {
                return View(vm);
          }

            var origin = Request.Headers["origin"];

            if(vm.TypeOfUser == "Agente")
            {
                RegistrerResponse response =  await _userServices.RegisterAgenteAsync(vm, origin);

                if (response.HasError)
                {
                    vm.HasError = response.HasError;
                    vm.Error = response.Error;
                    ModelState.AddModelError("Repuesta", $"{vm.Error}");
                    return View(vm);
                }

                var user = await _userServices.GetUserById(response.userId);

                SaveAgenteViewModel agenteVm = new();
                agenteVm.ImgUrl = user.ImgUrl;
                agenteVm.Telefono = user.PhoneNumber;
                agenteVm.IdentityId = user.UserId;
                agenteVm.Nombre = user.FirstName;
                agenteVm.Apellido = user.LastName;  
                agenteVm.Correo = user.Email;
                agenteVm.IsActive = false;

                await _agenteService.AddAsync(agenteVm);    

                if (response.HasError)
                {
                    vm.HasError = response.HasError;
                    vm.Error = response.Error;
                    ModelState.AddModelError("Repuesta", $"{vm.Error}");
                    return View(vm);
                }
            }
            else if(vm.TypeOfUser == "Cliente")
            {
              RegistrerResponse response =  await _userServices.RegisterClienteAsync(vm, origin);

                var user = await _userServices.GetUserById(response.userId);

                SaveClienteViewModel clienteVm = new();
                clienteVm.IdentityId = user.UserId;
                clienteVm.Nombre = user.FirstName;
                clienteVm.Apellido = user.LastName; 
                clienteVm.Correo = user.Email;
                clienteVm.IsActive = false;

                await _clienteService.AddAsync(clienteVm);

                if (response.HasError)
                {
                    vm.HasError = response.HasError;
                    vm.Error = response.Error;
                    return View(User);
                }
            }

            return RedirectToRoute(new { controller = "User", action = "Login" });

        }
        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            AuthenticationResponse userAuthenticate = await _userServices.LoginAsync(vm);

            if (userAuthenticate != null && userAuthenticate.HasError != true)
            {
                HttpContext.Session.set<AuthenticationResponse>("User", userAuthenticate);

                var userRole = userAuthenticate.Roles[0];

                if (userRole == "Cliente")
                {
                    return RedirectToAction("Index", "Home");
                }
                
                else if(userRole == "Admin")
                {
                    return RedirectToAction("Index", "Admin");
                }

                else if(userRole == "Agente")
                {
                    return RedirectToAction("PropiedadesDelAgente", "Propiedad" , new {userId = userAuthenticate.Id});
                }

                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            else
            {
                vm.HasError = userAuthenticate.HasError;
                vm.Error = userAuthenticate.Error;
                return View(vm);
            }
        }
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            string response = await _userServices.ConfirmEmailAsync(userId, token);

            return View("ConfirmEmail", response);
        }
        public async Task<IActionResult> CerrarSesion()
        {
            await _userServices.SignOutAsync();
            HttpContext.Session.Remove("User");

            return RedirectToAction("Index", "Home");
        }
        public async Task <IActionResult> EditUser(string userId)
        {
            var user = await _userServices.GetUserById(userId);

            EditUserViewModel editVm = new();
            editVm.PhoneNumber = user.PhoneNumber;
            editVm.FirstName = user.FirstName;  
            editVm.LastName = user.LastName;
            editVm.ImgUrl = user.ImgUrl;
            editVm.Id = userId;

            return View(editVm);  
        }
        [HttpPost]
        public async Task <IActionResult> EditUser(EditUserViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            await _userServices.EditUser(vm);

            return View(vm);
        }
    }
}
