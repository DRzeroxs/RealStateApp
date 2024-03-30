using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Dto.Propiedades;
using RealStateApp.Core.Application.Dto.TipoDePropiedad;
using RealStateApp.Core.Application.Features.Propiedades.Queries.GetAllPropiedad;
using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Application.Wrappers;
using RealStateApp.Core.Domain.Entities.Descripcion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.TipoPropiedades.Queries.GetAllTiposPropiedad
{
    public class GetAllTiposPropiedadQuery : IRequest<IList<TipoPropiedadDto>>
    {

    }
    public class GetAllTiposPropiedadQueryHandler : IRequestHandler<GetAllTiposPropiedadQuery, IList<TipoPropiedadDto>>
    {
        private readonly ITipoPropiedadRepository _tipoPropiedadRepository;
        private readonly IMapper _mapper;
        public GetAllTiposPropiedadQueryHandler(ITipoPropiedadRepository tipoPropiedadRepository, IMapper mapper)
        {
            _tipoPropiedadRepository = tipoPropiedadRepository;
            _mapper = mapper;
        }

        public async Task<IList<TipoPropiedadDto>> Handle(GetAllTiposPropiedadQuery request, CancellationToken cancellationToken)
        {
            var tiposPropiedad = await GetAllTiposPropiedad();
            if (tiposPropiedad == null || tiposPropiedad.Count == 0) throw new Exception("There is not Tipos de Propiedad");
            return tiposPropiedad;
        }

        private async Task<List<TipoPropiedadDto>> GetAllTiposPropiedad()
        {
            var tiposPropiedad = await _tipoPropiedadRepository.GetAll();
            return tiposPropiedad.Select(tiposPropiedad => new TipoPropiedadDto()
            {
                Id = tiposPropiedad.Id,
                Nombre = tiposPropiedad.Nombre,
                Descripcion = tiposPropiedad.Descripcion
            }).ToList();
        }
    }
}
