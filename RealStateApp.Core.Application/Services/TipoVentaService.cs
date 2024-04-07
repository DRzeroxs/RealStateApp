using AutoMapper;
using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Application.Interfaces.IServices;
using RealStateApp.Core.Application.ViewModel.Propiedad;
using RealStateApp.Core.Application.ViewModel.TipoVenta;
using RealStateApp.Core.Domain.Entities.Descripcion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Services
{
    public class TipoVentaService : GenericServices<TipoVentaViewModel, SaveTipoVentaViewModel, TipoVenta>, ITipoVentaService
    {
        private readonly ITipoVentaRepository _repository;
        private readonly IPropiedadRepository _propiedadRepository;
        private readonly IMapper _mapper;
        public TipoVentaService(ITipoVentaRepository repository, IMapper mapper, IPropiedadRepository propiedadRepository) : base(repository, mapper)
        {
             _repository = repository;
            _mapper = mapper;   
            _propiedadRepository = propiedadRepository;
        }
        public async Task<List<TipoVentaViewModel>> GetTipoVentasAsync()
        {
            var tipoVentaList = await _repository.GetAll();
            var propiedadesList = await _propiedadRepository.GetAll();

            var tipoPropiedades = from tv in tipoVentaList 
                                  select new TipoVentaViewModel
                                  {
                                      Id = tv.Id,
                                      Nombre = tv.Nombre,
                                      Descripcion = tv.Descripcion,
                                      CountPropiedades = (from p in propiedadesList
                                                          where p.TipoPropiedadId == tv.Id
                                                          select new PropiedadViewModel { Id = p.Id }).Count()
                                  };

            return tipoPropiedades.ToList();
        }
    }
}
