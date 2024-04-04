﻿using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Dto.Propiedades;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using IPropiedadService = RealStateApp.Core.Application.Interfaces.IServices.IPropiedadService;

namespace RealStateApp.Core.Application.Features.Propiedades.Queries.GetAllPropiedadesByCode
{
    // <summary>
    // Obtener una propiedad por el codigo de dicha propiedad
    // </summary>
    public class GetAllPropiedadesByCode : IRequest<Response<PropiedadesDto>>
    {
        [SwaggerParameter(Description = "Identificador de la propiedad que desea obtener")]
        public int identifier { get; set; } 
    }

    public class GetAllPropiedadesByCodeHanlder : IRequestHandler<GetAllPropiedadesByCode, Response<PropiedadesDto>>
    {
        private readonly IPropiedadService _service;
        private readonly IMapper _mapper;
        public GetAllPropiedadesByCodeHanlder(IPropiedadService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;   
        }

        public async Task<Response<PropiedadesDto>> Handle(GetAllPropiedadesByCode request, CancellationToken cancellationToken)
        {
            var propiedades = await GetAllPropiedadesByCode(request.identifier);

            if (propiedades is null) throw new ApiExeption("No existe esa propiedad", (int)HttpStatusCode.NotFound);

            return propiedades;
        }

        public async Task<Response<PropiedadesDto>> GetAllPropiedadesByCode(int identifier)
        {
            var propiedades = await _service.GetAllPropiedadesByCode(identifier);

            PropiedadesDto propiedadesDto = _mapper.Map<PropiedadesDto>(propiedades);

            return new Response<PropiedadesDto>( propiedadesDto);
        }

        
    }
}
