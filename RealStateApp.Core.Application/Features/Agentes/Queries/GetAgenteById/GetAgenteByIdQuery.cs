using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Dto.Agente;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Application.Wrappers;
using RealStateApp.Core.Domain.Entities;
using RealStateApp.Core.Domain.Entities.Users;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.Agentes.Queries.GetAgenteById
{
    // <summary>
    // Parametros para obtener un agente por el id
    // </summary>
    public class GetAgenteByIdQuery : IRequest<Response<AgenteDto>>
    {
        [SwaggerParameter(Description = "Id del agente que desea obtener")]
        public int Id { get; set; }
    }

    public class GetAgenteByIdQueryHandler : IRequestHandler<GetAgenteByIdQuery, Response<AgenteDto>>
    {
        private readonly IAgenteRepository _agenteRepository;
        private readonly IMapper _mapper;
        public GetAgenteByIdQueryHandler(IAgenteRepository agenteRepository, IMapper mapper)
        {
            _agenteRepository = agenteRepository;
            _mapper = mapper;
        }
        public async Task<Response<AgenteDto>> Handle(GetAgenteByIdQuery request, CancellationToken cancellationToken)
        {
            var agente = await GetAgenteById(request.Id);
            if (agente == null) throw new ApiExeption("No se encontro ese agente", (int)HttpStatusCode.NotFound);
            return new Response<AgenteDto>(agente);
        }

        private async Task<AgenteDto> GetAgenteById(int id)
        {
            var agente = await _agenteRepository.GetById(id);
            if (agente != null)
            {
                var agenteCantidadPropiedad = await _agenteRepository.GetCantidadPropiedadAgente();
                AgenteDto agenteDto = new AgenteDto()
                {
                    Id = agente.Id,
                    Nombre = agente.Nombre,
                    Apellido = agente.Apellido,
                    Telefono = agente.Telefono,
                    Correo = agente.Correo,
                    CantidadPropiedades = agenteCantidadPropiedad,
                };

                return agenteDto;
            }
            else
            {
                return null;
            }


        }
    }
}
