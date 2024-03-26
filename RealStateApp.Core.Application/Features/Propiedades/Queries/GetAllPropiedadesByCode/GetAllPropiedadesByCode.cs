using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Dto.Propiedades;
using RealStateApp.Core.Application.Interfaces.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.Propiedades.Queries.GetAllPropiedadesByCode
{
    public class GetAllPropiedadesByCode : IRequest<PropiedadesDto>
    {
        public int identifier { get; set; } 
    }

    public class GetAllPropiedadesByCodeHanlder : IRequestHandler<GetAllPropiedadesByCode, PropiedadesDto>
    {
        private readonly IPropiedadRepository _repository;
        private readonly IMapper _mapper;
        public GetAllPropiedadesByCodeHanlder(IPropiedadRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;   
        }

        public async Task<PropiedadesDto> Handle(GetAllPropiedadesByCode request, CancellationToken cancellationToken)
        {
            var propiedades = await GetAllPropiedadesByCode(request.identifier);

            if (propiedades is null) throw new Exception("No existe una propiedad con ese codigo");

            return propiedades;
        }

        public async Task<PropiedadesDto> GetAllPropiedadesByCode(int identifier)
        {
            var propiedades = await _repository.GetAllPropiedadesByCode(identifier);

            PropiedadesDto propiedadesDto = _mapper.Map<PropiedadesDto>(propiedades);

            return propiedadesDto;
        }

        
    }
}
