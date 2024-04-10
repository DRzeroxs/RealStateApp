using AutoMapper;
using RealStateApp.Core.Application.Dto.Agente;
using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Application.Interfaces.IServices;
using RealStateApp.Core.Application.ViewModel.AppUsers.Agente;
using RealStateApp.Core.Application.ViewModel.TipoPropiedad;
using RealStateApp.Core.Application.ViewModel.TipoVenta;
using RealStateApp.Core.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Services
{
    public class AgenteService : GenericServices<AgenteViewModel, SaveAgenteViewModel, Agente>, IAgenteService
    {
        private readonly IAgenteRepository _agenteRepository;
        private readonly IMapper _mapper;

        public AgenteService(IAgenteRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _agenteRepository = repository;
            _mapper = mapper;
        }

        public override async Task<List<AgenteViewModel>> GetAllAsync()
        {
            var agente = await _agenteRepository.GetAll();
            return agente.Select(x => new AgenteViewModel
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Apellido = x.Apellido,
            }).OrderBy(x => x.Nombre).ToList();
        }
        public async Task<AgenteViewModel> GetAgenteByNombre(string nombre)
        {
            var agente = await _agenteRepository.GetAgenteByNombre(nombre);
            AgenteViewModel avm = new AgenteViewModel();
            if (agente != null)
            {
                avm.Id = agente.Id;
                avm.Nombre = agente.Nombre;
                avm.Apellido = agente.Apellido;
                avm.ImgUrl = agente.ImgUrl;
                return avm;
            }
            else
            {
                avm.HasError = true;
                return avm;
            }

        }
    }
}
