using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Dto.Propiedades;
using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Application.ViewModel.Propiedad;
using RealStateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.Propiedades.Queries.GetAllPropiedad
{
    public class GetAllPropiedadQuery : IRequest<IList<PropiedadesDto>>
    {
    }

    public class GetAllPropiedadQueryHandler : IRequestHandler<GetAllPropiedadQuery, IList<PropiedadesDto>>
    {
        private readonly IPropiedadRepository _repository;
        private readonly IMapper _mapper;
        public GetAllPropiedadQueryHandler(IPropiedadRepository repository, IMapper mapper)
        {
            _repository = repository;   
            _mapper = mapper;   
        }
        public async Task<IList<PropiedadesDto>> Handle(GetAllPropiedadQuery request, CancellationToken cancellationToken)
        {
            var propiedades = await _repository.GetAllPropiedades();

            var propiedadesList = _mapper.Map<IList<Propiedad>, IList<PropiedadesDto>>(propiedades);

            return propiedadesList;
        }
    }
}
