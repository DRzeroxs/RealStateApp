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

namespace RealStateApp.Core.Application.Features.Propiedades.Queries.GetPropiedadById
{
    public class GetPropiedadByIdQuery : IRequest<Response<PropiedadesDto>>
    {
        public int Id { get; set; } 
    }
    public class GetPropiedadByIdQueryHandler : IRequestHandler<GetPropiedadByIdQuery, Response<PropiedadesDto>>
    {
        private readonly IPropiedadRepository _repository;
        private readonly IMapper _mapper;
        public GetPropiedadByIdQueryHandler(IPropiedadRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;   
        }
        public async Task<Response<PropiedadesDto>> Handle(GetPropiedadByIdQuery request, CancellationToken cancellationToken)
        {
            var propiedad = await GetPropiedadById(request.Id);

            if (propiedad == null) throw new ApiEception("No existe una propiedad con ese Id", (int)HttpStatusCode.NotFound);

            return new Response<PropiedadesDto>(propiedad);
        }
        public async Task<PropiedadesDto> GetPropiedadById(int Id)
        {
            var propiedad = await _repository.GetPropiedadesById(Id);

            PropiedadesDto propiedadDto = _mapper.Map<PropiedadesDto>(propiedad);

            return  propiedadDto;
        }

        
    }
}
