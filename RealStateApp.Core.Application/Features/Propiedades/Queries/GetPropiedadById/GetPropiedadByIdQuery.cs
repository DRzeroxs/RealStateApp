using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Dto.Propiedades;
using RealStateApp.Core.Application.Interfaces.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.Propiedades.Queries.GetPropiedadById
{
    public class GetPropiedadByIdQuery : IRequest<PropiedadesDto>
    {
        public int Id { get; set; } 
    }
    public class GetPropiedadByIdQueryHandler : IRequestHandler<GetPropiedadByIdQuery, PropiedadesDto>
    {
        private readonly IPropiedadRepository _repository;
        private readonly IMapper _mapper;
        public GetPropiedadByIdQueryHandler(IPropiedadRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;   
        }
        public async Task<PropiedadesDto> Handle(GetPropiedadByIdQuery request, CancellationToken cancellationToken)
        {
            var propiedad = await GetPropiedadById(request.Id);

            if (propiedad == null) throw new Exception("No existe la Propiedad");

            return propiedad;
        }
        public async Task<PropiedadesDto> GetPropiedadById(int Id)
        {
            var propiedad = await _repository.GetPropiedadesById(Id);

            PropiedadesDto propiedadDto = _mapper.Map<PropiedadesDto>(propiedad);

            return propiedadDto;
        }

        
    }
}
