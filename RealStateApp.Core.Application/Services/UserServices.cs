using AutoMapper;
using RealStateApp.Core.Application.Dto.Account;
using RealStateApp.Core.Application.Interfaces.IAccount;
using RealStateApp.Core.Application.Interfaces.IServices;
using RealStateApp.Core.Application.ViewModel.AppUsers.Agente;
using RealStateApp.Core.Application.ViewModel.User;
using RealStateApp.Core.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Services
{
    public class UserServices : IUserServices
    {
        private readonly IAccountService _accountServices;
        private readonly IMapper _mapper;
        private readonly IAgenteService _agenteService;
        public UserServices(IAccountService accountServices, IMapper mapper, IAgenteService agenteService)
        {
            _accountServices = accountServices;
            _mapper = mapper;
            _agenteService = agenteService;
        }

        // Metodo de logueo
        public async Task<AuthenticationResponse> LoginAsync(LoginViewModel loginVm)
        {
            AuthenticationRequest loginRequest = _mapper.Map<AuthenticationRequest>(loginVm);
            AuthenticationResponse authenticationResponse = await _accountServices.AuthenticateASYNC(loginRequest);
            return authenticationResponse;
        }
        // Metodo para Registrar Usuario Cliente
        public async Task<RegistrerResponse> RegisterClienteAsync(RegistrerViewModel vm, string origin)
        {
            RegistrerRequest registerRequest = _mapper.Map<RegistrerRequest>(vm);


            return await _accountServices.RegistrerClienteUserAsync(registerRequest, origin);
        }

        // Metodo para Registrar Usuario Agente
        public async Task<RegistrerResponse> RegisterAgenteAsync(RegistrerViewModel vm, string origin)
        {

            RegistrerRequest registerRequest = _mapper.Map<RegistrerRequest>(vm);


            return await _accountServices.RegistrerAgenteUserAsync(registerRequest, origin);
        }
        public async Task<RegistrerResponse> RegisterAdminAsync(RegistrerViewModel vm, string origin)
        {

            RegistrerRequest registerRequest = _mapper.Map<RegistrerRequest>(vm);


            return await _accountServices.RegistrerAdminUserAsync(registerRequest, origin);
        }

        // Metodo de deslogueo
        public async Task SignOutAsync()
        {
            await _accountServices.SingOutAsync();
        }
        // Metodo para Confirmar usuario por Correo
        public async Task<string> ConfirmEmailAsync(string userId, string token)
        {

            return await _accountServices.ConfirmAccountAsync(userId, token);
        }

        // Metodo para Buscar Usuario Por el Id tipo String
        public async Task<UserViewModel> GetUserById(string userId)
        {
            var user = await _accountServices.GetById(userId);

            return user;    
        }

        // Metodo para Editar Usuarios
        public async Task<EditUserViewModel> EditUser(EditUserViewModel vm)
        {
            await _accountServices.EditUser(vm);

            return vm;  
        }

        // Metodo para Contar Agentes Activos
        public async Task<int> ContarAgentesActivos()
        {
            var count = await _accountServices.CountAgentesActivos();

            return count;
        }

        // Metodo para Contar Agentes Inactivos
        public async Task<int> ContarAgentesInactivos()
        {
            var count = await _accountServices.CountAgentesInactivos();

            return count;
        }

        // Metodo para Contar Clientes Activos
        public async Task<int> ContarClientesActivos()
        {
            var count = await _accountServices.CountClientesActivos();

            return count;
        }
        // Metodo para Contar Clientes Inactivos
        public async Task<int> ContarClientesInactivos()
        {
            var count = await _accountServices.CountClientesInactivos();

            return count;
        }

        // Metodo para Contar Desarrolladores Inactivos
        public async Task<int> ContarDesarrolladoresInactivos()
        {
            var count = await _accountServices.CountDesarrolladoresInactivos();

            return count;
        }
        // Metodo para Contar Desarrolladores Activos
        public async Task<int> ContarDesarrolladoresActivos()
        {
            var count = await _accountServices.CountDesarrolladoresActivos();

            return count;
        }

        // Metodo Para Eliminar Agentes
        public async Task EliminarAgente(string userId)
        {
            var agenteVm = await _agenteService.GetByIdentityId(userId);

            await _agenteService.RemoveAsync(agenteVm.Id);

            await _accountServices.EliminarAgente(userId);
        }

        // Metodo Para traer Lista de Usuarios Administradores
        public async Task<List<UserViewModel>> GetUsuariosAdministradores()
        {
            var users = await _accountServices.GetUsuariosAdministrador();

            return users;
        }
        // Metodo Para traer Lista de Usuarios Desarrolladores
        public async Task<List<UserViewModel>> GetUsuariosDesarrolladores()
        {
            var users = await _accountServices.GetUsuariosDesarrollador();

            return users;
        }

        // Metodo para Activar Agente
        public async Task ActivarAgente(string userId)
        {
            var agente = await _agenteService.GetByIdentityId(userId);
            SaveAgenteViewModel agenteSave = _mapper.Map<SaveAgenteViewModel>(agente);

            agenteSave.IsActive = true;

            await _agenteService.UpdateAsync(agenteSave, agenteSave.Id);

            await _accountServices.ActivarAgente(userId);

        }

        // Metodo para Inactivar Agente
        public async Task InactivarAgente(string userId)
        {
            var agente = await _agenteService.GetByIdentityId(userId);
            SaveAgenteViewModel agenteSave = _mapper.Map<SaveAgenteViewModel>(agente);

            agenteSave.IsActive = false;

            await _agenteService.UpdateAsync(agenteSave, agenteSave.Id);

            await _accountServices.IanctivarAgente(userId);
        }

        // Metodo para Activar Usuario Administrador
        public async Task ActivarAdmin(string userId)
        {
            await _accountServices.ActivarAdmin(userId);    
        }

        // Metodo para Inactivar Usuario Administrador
        public async Task InactivarAdmin(string userId)
        {
            await _accountServices.InactivarAdmin(userId);
        }

        // Metodo para Editar Usuario Administrador
        public async Task EditarUsuarioAdmin(UserPostViewModel vm)
        {
            await _accountServices.EditarAdmin(vm);
        }

        // Metodo para Crear Usuario Desarrollador
        public async Task<RegistrerResponse> RegisterDesarrolladorAsync(RegistrerViewModel vm)
        {

            RegistrerRequest registerRequest = _mapper.Map<RegistrerRequest>(vm);


            return await _accountServices.RegistrerDesarrolladorAsync(registerRequest);
        }

        // Metodo para Inactivar Desarrollador
        public async Task InactivarDesarrollador(string userId)
        {
            await _accountServices.InactivarDesarrollador(userId);
        }

        // Metodo para Activar Desarrollador
        public async Task ActivarDesarrollador(string userId)
        {
            await _accountServices.ActivarDesarrollador(userId);
        }
    }
}
