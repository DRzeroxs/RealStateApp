using RealStateApp.Core.Application.Dto.Account;
using RealStateApp.Core.Application.ViewModel.User;

namespace RealStateApp.Core.Application.Interfaces.IAccount;

public interface IAccountService
{

    Task ActivarUsuario(string userId);
    Task AddUserDb();
    Task<AuthenticationResponse> AuthenticateASYNC(AuthenticationRequest requuest);
    Task ConfirmAccountAsync(string userId);
    Task<string> ConfirmAccountAsync(string userId, string token);
    Task<int> CountAgentesActivos();
    Task<int> CountAgentesInactivos();
    Task<int> CountClientesActivos();
    Task<int> CountClientesInactivos();
    Task<int> CountDesarrolladoresActivos();
    Task<int> CountDesarrolladoresInactivos();
    Task EditarUsuario(UserPostViewModel vm);
    Task EditUser(EditUserViewModel vm);
    Task EliminarAgente(string userId);
    Task<UserViewModel> GetById(string userId);
    Task<List<UserViewModel>> GetUsuariosAdministrador();
    Task<List<UserViewModel>> GetUsuariosDesarrollador();
    Task InactivarUsuario(string userId);
    Task<RegistrerResponse> RegistrerAdminUserAsync(RegistrerRequest request, string origin);
    Task<RegistrerResponse> RegistrerAgenteUserAsync(RegistrerRequest request, string origin);
    Task<RegistrerResponse> RegistrerClienteUserAsync(RegistrerRequest request, string origin);
    Task<RegistrerResponse> RegistrerDesarrolladorAsync(RegistrerRequest request);
    Task SingOutAsync();
}