using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Dto.Propiedades;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Interfaces.IRepository;
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
        private readonly IPropiedadRepository _repository;
        private readonly IMapper _mapper;
        public GetAllPropiedadesByCodeHanlder(IPropiedadRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;   
        }

        public async Task<Response<PropiedadesDto>> Handle(GetAllPropiedadesByCode request, CancellationToken cancellationToken)
        {
            var propiedades = await GetAllPropiedadesByCode(request.identifier);

            if (propiedades is null) throw new ApiEception("No existe esa propiedad", (int)HttpStatusCode.NotFound);

            return propiedades;
        }

        public async Task<Response<PropiedadesDto>> GetAllPropiedadesByCode(int identifier)
        {
            var propiedades = await _repository.GetAllPropiedadesByCode(identifier);

            PropiedadesDto propiedadesDto = _mapper.Map<PropiedadesDto>(propiedades);

            return new Response<PropiedadesDto>( propiedadesDto);
        }

        
    }
}
