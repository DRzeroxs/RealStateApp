using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Dto.Propiedades;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Interfaces.IServices;
using RealStateApp.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace RealStateApp.Core.Application.Features.Propiedades.Queries.GetPropiedadById
{
    // <summary>
    // Parametros para obtener una propiedad por el id
    // </summary>
    public class GetPropiedadByIdQuery : IRequest<Response<PropiedadesDto>>
    {
        [SwaggerParameter(Description = "Id de la propiedad que desea obtener")]
        public int Id { get; set; } 
    }
    public class GetPropiedadByIdQueryHandler : IRequestHandler<GetPropiedadByIdQuery, Response<PropiedadesDto>>
    {
        private readonly IPropiedadService _service;
        private readonly IMapper _mapper;
        public GetPropiedadByIdQueryHandler(IPropiedadService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;   
        }
        public async Task<Response<PropiedadesDto>> Handle(GetPropiedadByIdQuery request, CancellationToken cancellationToken)
        {
            var propiedad = await GetPropiedadById(request.Id);

            if (propiedad == null) throw new ApiExeption("No existe una propiedad con ese Id", (int)HttpStatusCode.NotFound);

            return new Response<PropiedadesDto>(propiedad);
        }
        public async Task<PropiedadesDto> GetPropiedadById(int Id)
        {
            var propiedad = await _service.GetPropiedadesById(Id);

            PropiedadesDto propiedadDto = _mapper.Map<PropiedadesDto>(propiedad);

            return  propiedadDto;
        }

        
    }
}
