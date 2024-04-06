using RealStateApp.Core.Application.Dto.Account;
using RealStateApp.Core.Application.ViewModel.User;

namespace RealStateApp.Core.Application.Interfaces.IAccount;

public interface IAccountService
{
    Task ActivarAdmin(string userId);
    Task ActivarAgente(string userId);
    Task<AuthenticationResponse> AuthenticateASYNC(AuthenticationRequest requuest);
    Task ConfirmAccountAsync(string userId);
    Task<string> ConfirmAccountAsync(string userId, string token);
    Task<int> CountAgentesActivos();
    Task<int> CountAgentesInactivos();
    Task<int> CountClientesActivos();
    Task<int> CountClientesInactivos();
    Task<int> CountDesarrolladoresActivos();
    Task<int> CountDesarrolladoresInactivos();
    Task EditarAdmin(UserPostViewModel vm);
    Task EditUser(EditUserViewModel vm);
    Task EliminarAgente(string userId);
    Task<UserPostViewModel> GetById(string userId);
    Task<List<UserPostViewModel>> GetUsuariosAdministrador();
    Task IanctivarAgente(string userId);
    Task InactivarAdmin(string userId);
    Task<RegistrerResponse> RegistrerAdminUserAsync(RegistrerRequest request, string origin);
    Task<RegistrerResponse> RegistrerAgenteUserAsync(RegistrerRequest request, string origin);
    Task<RegistrerResponse> RegistrerClienteUserAsync(RegistrerRequest request, string origin);
    Task SingOutAsync();
}