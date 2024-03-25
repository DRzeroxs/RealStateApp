using Azure;
using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Dto.Account;
using RealStateApp.Core.Application.Interfaces.IServices;
using RealStateApp.Core.Application.ViewModel.User;

namespace RealStateApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserServices _userServices;
        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
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
                    return View(User);
                }
            }
            else if(vm.TypeOfUser == "Cliente")
            {
              RegistrerResponse response =  await _userServices.RegisterClienteAsync(vm, origin);

                if (response.HasError)
                {
                    vm.HasError = response.HasError;
                    vm.Error = response.Error;
                    return View(User);
                }
            }

            return View();    

        }
    }
}
