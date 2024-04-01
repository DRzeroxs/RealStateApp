using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Dto.Mejora;
using RealStateApp.Core.Application.Dto.Propiedades;
using RealStateApp.Core.Application.Dto.TipoDePropiedad;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Features.Propiedades.Queries.GetAllPropiedad;
using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Application.Wrappers;
using RealStateApp.Core.Domain.Entities;
using RealStateApp.Core.Domain.Entities.Descripcion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.TipoPropiedades.Queries.GetAllTiposPropiedad
{
    // <summary>
    // Obtener todos los tipos de propiedades
    // </summary>
    public class GetAllTiposPropiedadQuery : IRequest<Response<IList<TipoPropiedadDto>>>
    {

    }
    public class GetAllTiposPropiedadQueryHandler : IRequestHandler<GetAllTiposPropiedadQuery, Response<IList<TipoPropiedadDto>>>
    {
        private readonly ITipoPropiedadRepository _tipoPropiedadRepository;
        private readonly IMapper _mapper;
        public GetAllTiposPropiedadQueryHandler(ITipoPropiedadRepository tipoPropiedadRepository, IMapper mapper)
        {
            _tipoPropiedadRepository = tipoPropiedadRepository;
            _mapper = mapper;
        }

        public async Task<Response<IList<TipoPropiedadDto>>> Handle(GetAllTiposPropiedadQuery request, CancellationToken cancellationToken)
        {
            var tiposPropiedad = await _tipoPropiedadRepository.GetAll();
            if (tiposPropiedad == null || tiposPropiedad.Count == 0) throw new ApiExeption("No hay tipos de propiedad", (int)HttpStatusCode.NotFound);
            return new Response<IList<TipoPropiedadDto>>(_mapper.Map<IList<TipoPropiedadDto>>(tiposPropiedad));
        }
    }
}
