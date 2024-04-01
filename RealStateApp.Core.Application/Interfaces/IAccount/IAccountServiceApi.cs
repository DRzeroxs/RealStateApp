using RealStateApp.Core.Application.Dto.Account;
using RealStateApp.Core.Application.ViewModel.User;

namespace RealStateApp.Core.Application.Interfaces.IAccount;

public interface IAccountServiceApi
{
    Task<AuthenticationResponse> AuthenticateASYNC(AuthenticationRequest requuest);
    Task<string> ConfirmAccountAsync(string userId, string token);
    Task<RegistrerResponse> RegistroUsuarioAdministrador(RegistrerRequest request, string origin);
    Task<RegistrerResponse> RegistroUsuarioDesarrollador(RegistrerRequest request, string origin);
    Task SingOutAsync();
    Task<bool> InactiveAccountAsync(string userId);
    Task<bool> ActiveAccountAsync(string userId);
    Task<UserPostViewModel> GetById(string Id);
}