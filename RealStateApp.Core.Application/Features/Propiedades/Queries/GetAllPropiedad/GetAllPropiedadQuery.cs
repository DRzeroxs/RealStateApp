using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Dto.Propiedades;
using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Application.Interfaces.IServices;
using RealStateApp.Core.Application.ViewModel.Propiedad;
using RealStateApp.Core.Application.Wrappers;
using RealStateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.Propiedades.Queries.GetAllPropiedad
{
    // <summary>
    // Obtener todas las propiedades
    // </summary>
    public class GetAllPropiedadQuery : IRequest<Response<IList<PropiedadesDto>>>
    {
    }

    public class GetAllPropiedadQueryHandler : IRequestHandler<GetAllPropiedadQuery, Response<IList<PropiedadesDto>>>
    {
        private readonly IPropiedadService _service;
        private readonly IMapper _mapper;
        public GetAllPropiedadQueryHandler(IPropiedadService service, IMapper mapper)
        {
            _service = service;   
            _mapper = mapper;   
        }
        public async Task<Response<IList<PropiedadesDto>>> Handle(GetAllPropiedadQuery request, CancellationToken cancellationToken)
        {
            var propiedades = await _service.GetAllPropiedades();

            var propiedadesList = _mapper.Map<IList<PropiedadViewModel>, IList<PropiedadesDto>>(propiedades);

            return new Response<IList<PropiedadesDto>>(propiedadesList);
        }
    }
}
