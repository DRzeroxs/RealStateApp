using RealStateApp.Core.Application.Dto.Account;

namespace RealStateApp.Core.Application.Interfaces.IAccount;

public interface IAccountService
{
    Task<AuthenticationResponse> AuthenticateASYNC(AuthenticationRequest requuest);
    Task ConfirmAccountAsync(string userId);
    Task<RegistrerResponse> RegistrerAgenteUserAsync(RegistrerRequest request, string origin);
    Task<RegistrerResponse> RegistrerClienteUserAsync(RegistrerRequest request, string origin);
    Task SingOutAsync();
}