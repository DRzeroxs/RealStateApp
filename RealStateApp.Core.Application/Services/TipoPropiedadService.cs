using AutoMapper;
using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Application.Interfaces.IServices;
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
        private readonly IMapper _mapper;
        public TipoPropiedadService(ITipoPropiedadRepository repository, IMapper mapper) : base(repository, mapper)
        {
             _repository = repository;
            _mapper = mapper;   
        }
    }
}
