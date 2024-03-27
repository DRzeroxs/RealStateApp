using AutoMapper;
using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Application.Interfaces.IServices;
using RealStateApp.Core.Application.ViewModel.AppUsers.Administrador;
using RealStateApp.Core.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Services
{
    public class AdministradorService : GenericServices<AdministradorViewModel, SaveAdministradorViewModel, Administrador>, IAdministradorService
    {
        private readonly IAdministradorRepository _repository;
        private readonly IMapper _mapper;
        public AdministradorService(IAdministradorRepository repository, IMapper mapper) : base(repository, mapper)
        {
             _repository = repository;
            _mapper = mapper;   
        }
    }
}
