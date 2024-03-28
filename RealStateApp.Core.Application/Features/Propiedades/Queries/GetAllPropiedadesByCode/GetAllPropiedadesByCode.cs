using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Dto.Propiedades;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Application.Interfaces.IServices;
using RealStateApp.Core.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace RealStateApp.Core.Application.Features.Propiedades.Queries.GetAllPropiedadesByCode
{
    public class GetAllPropiedadesByCode : IRequest<Response<PropiedadesDto>>
    {
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
            var propiedades = await GetAllPropiedadesByCodeAsync(request.identifier);

            if (propiedades is null) throw new ApiEception("No existe esa propiedad", (int)HttpStatusCode.NotFound);

            return propiedades;
        }

        public async Task<Response<PropiedadesDto>> GetAllPropiedadesByCodeAsync(int identifier)
        {
            var propiedades = await _service.GetAllPropiedadesByCode(identifier);

            PropiedadesDto propiedadesDto = _mapper.Map<PropiedadesDto>(propiedades);

            return new Response<PropiedadesDto>( propiedadesDto);
        }

        
    }
}
