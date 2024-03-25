using AutoMapper;
using RealStateApp.Core.Application.Dto.Account;
using RealStateApp.Core.Application.Interfaces.IAccount;
using RealStateApp.Core.Application.Interfaces.IServices;
using RealStateApp.Core.Application.ViewModel.User;
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

        public UserServices(IAccountService accountServices, IMapper mapper)
        {
            _accountServices = accountServices;
            _mapper = mapper;
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

        // Metodo de deslogueo
        public async Task SignOutAsync()
        {
            await _accountServices.SingOutAsync();
        }

    }
}
