using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Dto.Propiedades;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Interfaces.IServices;
using RealStateApp.Core.Application.ViewModel.Propiedad;
using RealStateApp.Core.Application.Wrappers;
using RealStateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.Propiedades.Queries.GetAllPropiedadesByCode
{
    public class GetPropiedadByCodeQuery : IRequest<Response<PropiedadesDto>>
    {
        public int Identifier { get; set; } 
    }
    public class GetPropiedadByCodeQueryHandler : IRequestHandler<GetPropiedadByCodeQuery, Response<PropiedadesDto>>
    {
        private readonly IPropiedadService _service;
        private readonly IMapper _mapper;
        public GetPropiedadByCodeQueryHandler(IPropiedadService service, IMapper mapper)
        {
            _service = service; 
            _mapper = mapper;
        }
        public async Task<Response<PropiedadesDto>> Handle(GetPropiedadByCodeQuery request, CancellationToken cancellationToken)
        {
            var propiedad = await GetPropiedadByCode(request.Identifier);

            if (propiedad is null) throw new ApiEception("No existe esa propiedad", (int)HttpStatusCode.NotFound);

            return propiedad;
        }
        private async Task<Response<PropiedadesDto>> GetPropiedadByCode(int identifier)
        {
            var propiedad = await _service.GetAllPropiedadesByCode(identifier);

            PropiedadesDto dto = _mapper.Map<PropiedadesDto>(propiedad);

            return new Response<PropiedadesDto> (dto);
        }
    }
}
