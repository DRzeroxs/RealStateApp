using AutoMapper;
using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Application.Interfaces.IServices;
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
        private readonly IMapper _mapper;
        public TipoVentaService(ITipoVentaRepository repository, IMapper mapper) : base(repository, mapper)
        {
             _repository = repository;
            _mapper = mapper;   
        }
    }
}
