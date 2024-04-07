using AutoMapper;
using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Application.Interfaces.IServices;
using RealStateApp.Core.Application.ViewModel.Propiedad;
using RealStateApp.Core.Application.ViewModel.TipoPropiedad;
using RealStateApp.Core.Domain.Entities.Descripcion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Services
{
    public class TipoPropiedadService : GenericServices<TipoPropiedadViewModel, SaveTipoPropiedadViewModel, TipoPropiedad>, ITipoPropiedadService
    {
        private readonly ITipoPropiedadRepository _repository;
        private readonly IPropiedadRepository _propiedadRepository;
        private readonly IMapper _mapper;
        public TipoPropiedadService(ITipoPropiedadRepository repository, IMapper mapper, IPropiedadRepository propiedadRepository) : base(repository, mapper)
        {
             _repository = repository;
            _mapper = mapper;   
            _propiedadRepository = propiedadRepository; 
        }
        public async Task<List<TipoPropiedadViewModel>> GetTipoPropiedadAsync()
        {
            var tipoPropiedadList = await _repository.GetAll();
            var propiedadesList = await _propiedadRepository.GetAll();

            var tipoPropiedades = from tp in tipoPropiedadList
                                  select new TipoPropiedadViewModel
                                  {
                                      Id = tp.Id,   
                                      Nombre = tp.Nombre,
                                      Descripcion = tp.Descripcion,
                                      CountPropiedades = (from p in propiedadesList
                                                          where p.TipoPropiedadId == tp.Id
                                                          select new PropiedadViewModel {Id = p.Id }).Count()
                                  };

            return tipoPropiedades.ToList();
        }
    }
}
