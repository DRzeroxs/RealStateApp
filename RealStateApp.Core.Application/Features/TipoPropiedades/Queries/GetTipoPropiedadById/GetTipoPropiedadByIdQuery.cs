using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Dto.TipoDePropiedad;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.TipoPropiedades.Queries.GetTipoPropiedadById
{
    // <summary>
    // Parametros para obtener un tipo de propiedad por id
    // </summary>
    public class GetTipoPropiedadByIdQuery : IRequest<Response<TipoPropiedadDto>>
    {
        [SwaggerParameter(Description = "Id del tipo de propiedad que desea obtener")]
        public int Id { get; set; }
    }

    public class GetTipoPropiedadByIdQueryHandler : IRequestHandler<GetTipoPropiedadByIdQuery, Response<TipoPropiedadDto>>
    {
        private readonly ITipoPropiedadRepository _tipoPropiedadRepository;
        private readonly IMapper _mapper;
        public GetTipoPropiedadByIdQueryHandler(ITipoPropiedadRepository tipoPropiedadRepository, IMapper mapper)
        {
            _tipoPropiedadRepository = tipoPropiedadRepository;
            _mapper = mapper;
        }
        public async Task<Response<TipoPropiedadDto>> Handle(GetTipoPropiedadByIdQuery request, CancellationToken cancellationToken)
        {
            var tipoPropiedad = await _tipoPropiedadRepository.GetById(request.Id);
            if (tipoPropiedad == null) throw new ApiExeption("No se encontro un tipo de propiedad por ese id", (int)HttpStatusCode.NotFound);
            return new Response<TipoPropiedadDto>(_mapper.Map<TipoPropiedadDto>(tipoPropiedad));
        }

    }
}
