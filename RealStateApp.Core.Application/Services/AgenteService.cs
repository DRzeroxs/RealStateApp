using AutoMapper;
using RealStateApp.Core.Application.Dto.Agente;
using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Application.Interfaces.IServices;
using RealStateApp.Core.Application.ViewModel.AppUsers.Agente;
using RealStateApp.Core.Application.ViewModel.TipoPropiedad;
using RealStateApp.Core.Application.ViewModel.TipoVenta;
using RealStateApp.Core.Application.ViewModel.Propiedad;
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

        private readonly IAgenteRepository _repository;
        private readonly IPropiedadRepository _propiedadRepository;

        private readonly IMapper _mapper;

        public AgenteService(IAgenteRepository repository, IMapper mapper, IPropiedadRepository propiedadRepository) : base(repository, mapper)
        {

            _repository = repository;
            _propiedadRepository = propiedadRepository;
            _mapper = mapper;
        }

        public override async Task<List<AgenteViewModel>> GetAllAsync()
        {
            var agente = await _repository.GetAll();
            return agente.Select(x => new AgenteViewModel
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Apellido = x.Apellido,
                IdentityId = x.IdentityId,
                ImgUrl = x.ImgUrl,
                Cedula = x.Cedula,
                Correo = x.Correo,
            }).OrderBy(x => x.Nombre).ToList();
        }
        public async Task<AgenteViewModel> GetAgenteByNombre(string nombre)
        {
            var agente = await _repository.GetAgenteByNombre(nombre);
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
        public async Task<List<AgenteViewModel>> GetAgenteConPropiedades()
        {
            var agentesList = await _repository.GetAll();
            var propiedades = await _propiedadRepository.GetAll();

            var agentes = from a in agentesList
                          join p in propiedades
                          on a.Id equals p.AgenteId into agentePropiedades
                          from ap in agentePropiedades.DefaultIfEmpty()
                          select new AgenteViewModel
                          {
                              Id = a.Id,
                              Nombre = a.Nombre,
                              Apellido = a.Apellido,
                              Correo = a.Correo,
                              Cedula = a.Cedula,
                              IdentityId = a.IdentityId,
                              IsActive = a.IsActive,
                              Propiedades = (ap == null ? new List<PropiedadViewModel>() :
                                             new List<PropiedadViewModel>
                                             {
                                         new PropiedadViewModel
                                         {
                                             Id = ap.Id,
                                             Precio = ap.Precio,
                                             Identifier = ap.Identifier
                                         }
                                             })
                          };

            return agentes.ToList();
        }
        public async Task<AgenteViewModel> GetByIdentityId(string IdentityId)
        {
            var agentesList = await _repository.GetAll();

            var agente = agentesList.FirstOrDefault(a => a.IdentityId == IdentityId);

            AgenteViewModel agenteVm = _mapper.Map<AgenteViewModel>(agente);

            return agenteVm;

        }
    }
}
