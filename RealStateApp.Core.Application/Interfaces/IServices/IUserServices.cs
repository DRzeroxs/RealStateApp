using RealStateApp.Core.Application.Dto.Account;
using RealStateApp.Core.Application.ViewModel.User;

namespace RealStateApp.Core.Application.Interfaces.IServices
{
    public interface IUserServices
    {
        Task<AuthenticationResponse> LoginAsync(LoginViewModel loginVm);
        Task<RegistrerResponse> RegisterAgenteAsync(RegistrerViewModel vm, string origin);
        Task<RegistrerResponse> RegisterClienteAsync(RegistrerViewModel vm, string origin);
        Task SignOutAsync();
    }
}