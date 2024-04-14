using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RealStateApp.Core.Application.Dto.Account;
using RealStateApp.Core.Application.Interfaces.IAccount;
using Swashbuckle.AspNetCore.Annotations;

namespace RealStateApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountServiceApi _accountServices;
        public AccountController(IAccountServiceApi accountServices)
        {
            _accountServices = accountServices;
        }

        [HttpPost("authenticate")]
        [SwaggerOperation(
            Summary = "Autenticar un usuario",
            Description = "Autenticar un usuario en el sistema"
            )]
        public async Task<IActionResult> AuthenticateAsync(AuthenticationRequest request)
        {
            return Ok(await _accountServices.AuthenticateASYNC(request));
        }

        [HttpPost("RegistroAdministrador")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(
            Summary = "Registra a un usuario administrador",
            Description = "Registra a un usuario administrador en el sistema"
            )]
        public async Task<IActionResult> RegistroAdministrador(RegistrerRequest request)
        {
            var origin = Request.Headers["Origin"];

            request.TypeOfUser = "Administrador";

            return Ok(await _accountServices.RegistroUsuarioAdministrador(request, origin));
        }

        [HttpPost("RegistroDesarrollador")]
        [SwaggerOperation(
            Summary = "Autenticar un usuario desarrollador",
            Description = "Autenticar un usuario desarrollador en el sistema"
            )]
        public async Task<IActionResult> RegistroDesarrollador(RegistrerRequest request)
        {
            var origin = Request.Headers["Origin"];

            request.TypeOfUser = "Developer";
   
             return Ok(await _accountServices.RegistroUsuarioDesarrollador(request, origin));
        }
    }
}
