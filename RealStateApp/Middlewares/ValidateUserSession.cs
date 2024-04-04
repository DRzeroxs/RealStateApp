using Microsoft.AspNetCore.Http;
using RealStateApp.Core.Application.Dto.Account;
using RealStateApp.Core.Application.Helpers;

namespace RealStateApp.Middlewares
{
    public class ValidateUserSession
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public ValidateUserSession(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public bool HasUser()
        {
            AuthenticationResponse usuarioViewModel = _contextAccessor.HttpContext.Session.get<AuthenticationResponse>("User");

            if (usuarioViewModel == null)
            {
                return false;
            }

            return true;
        }
    }
}
