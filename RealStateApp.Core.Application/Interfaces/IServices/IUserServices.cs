using RealStateApp.Core.Application.Dto.Account;
using RealStateApp.Core.Application.ViewModel.User;

namespace RealStateApp.Core.Application.Interfaces.IServices
{
    public interface IUserServices
    {
        Task InactivarAdmin(string userId);
        Task ActivarAgente(string userId);
        Task<string> ConfirmEmailAsync(string userId, string token);
        Task<int> ContarAgentesActivos();
        Task<int> ContarAgentesInactivos();
        Task<int> ContarClientesActivos();
        Task<int> ContarClientesInactivos();
        Task<int> ContarDesarrolladoresActivos();
        Task<int> ContarDesarrolladoresInactivos();
        Task<EditUserViewModel> EditUser(EditUserViewModel vm);
        Task EliminarAgente(string userId);
        Task InactivarAgente(string userId);
        Task<AuthenticationResponse> LoginAsync(LoginViewModel loginVm);
        Task<RegistrerResponse> RegisterAdminAsync(RegistrerViewModel vm, string origin);
        Task<RegistrerResponse> RegisterAgenteAsync(RegistrerViewModel vm, string origin);
        Task<RegistrerResponse> RegisterClienteAsync(RegistrerViewModel vm, string origin);
        Task SignOutAsync();
        Task ActivarAdmin(string userId);
        Task EditarUsuarioAdmin(UserPostViewModel vm);
        Task<UserViewModel> GetUserById(string userId);
        Task<List<UserViewModel>> GetUsuariosAdministradores();
        Task<List<UserViewModel>> GetUsuariosDesarrolladores();
        Task<RegistrerResponse> RegisterDesarrolladorAsync(RegistrerViewModel vm);
        Task InactivarDesarrollador(string userId);
        Task ActivarDesarrollador(string userId);
    }
}