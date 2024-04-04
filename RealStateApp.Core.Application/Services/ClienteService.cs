using AutoMapper;
using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Application.Interfaces.IServices;
using RealStateApp.Core.Application.ViewModel.AppUsers.Cliente;
using RealStateApp.Core.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Services
{
    public class ClienteService : GenericServices<ClienteViewModel, SaveClienteViewModel, Cliente>, IClienteService
    {
        private readonly IClienteRepository _repository;
        private readonly IMapper _mapper;
        public ClienteService(IClienteRepository repository, IMapper mapper) : base(repository, mapper)
        {
             _repository = repository;
            _mapper = mapper;   
        }

        public async Task<ClienteViewModel> GetClientePorIdentityId(string Identity)
        {
            var clientesList = await _repository.GetAll();

            var cliente = clientesList.FirstOrDefault(c => c.IdentityId == Identity);

            ClienteViewModel clienteVm = _mapper.Map<ClienteViewModel>(cliente);

            return clienteVm;   
        }
    }
}
