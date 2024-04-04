using RealStateApp.Core.Application.Dto.Account;
using RealStateApp.Core.Application.ViewModel.User;

namespace RealStateApp.Core.Application.Interfaces.IAccount;

public interface IAccountService
{
    Task<AuthenticationResponse> AuthenticateASYNC(AuthenticationRequest requuest);
    Task ConfirmAccountAsync(string userId);
    Task<string> ConfirmAccountAsync(string userId, string token);
    Task EditUser(EditUserViewModel vm);
    Task<UserPostViewModel> GetById(string userId);
    Task<RegistrerResponse> RegistrerAgenteUserAsync(RegistrerRequest request, string origin);
    Task<RegistrerResponse> RegistrerClienteUserAsync(RegistrerRequest request, string origin);
    Task SingOutAsync();
}