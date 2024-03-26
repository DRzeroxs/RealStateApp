using RealStateApp.Core.Application.Dto.Account;

namespace RealStateApp.Core.Application.Interfaces.IAccount;

public interface IAccountServiceApi
{
    Task<AuthenticationResponse> AuthenticateASYNC(AuthenticationRequest requuest);
    Task<string> ConfirmAccountAsync(string userId, string token);
    Task<RegistrerResponse> RegistroUsuarioAdministrador(RegistrerRequest request, string origin);
    Task<RegistrerResponse> RegistroUsuarioDesarrollador(RegistrerRequest request, string origin);
    Task SingOutAsync();
}