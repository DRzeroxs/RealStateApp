using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Dto.Agente;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.Agentes.Queries.GetAllAgentes
{
    // <summary>
    // Obtener todos los agentes
    // </summary>
    public class GetAllAgentesQuery : IRequest<Response<IList<AgenteDto>>>
    {
    }
    public class GetAllAgentesQueryHandler : IRequestHandler<GetAllAgentesQuery, Response<IList<AgenteDto>>>
    {
        private readonly IAgenteRepository _agenteRepository;
        public GetAllAgentesQueryHandler(IAgenteRepository agenteRepository)
        {
            _agenteRepository = agenteRepository;
        }

        public async Task<Response<IList<AgenteDto>>> Handle(GetAllAgentesQuery request, CancellationToken cancellationToken)
        {
            var agentes = await GetAllAgentes();
            if (agentes == null || agentes.Count == 0) throw new ApiExeption("No se encontrarons los agentes", (int)HttpStatusCode.NotFound);
            return new Response<IList<AgenteDto>>(agentes);
        }

        private async Task<List<AgenteDto>> GetAllAgentes()
        {
            var agentes = await _agenteRepository.GetAll();
            var agenteCantidadPropiedad = await _agenteRepository.GetCantidadPropiedadAgente();
            return agentes.Select(agente => new AgenteDto
            {
                Id = agente.Id,
                Nombre = agente.Nombre,
                Apellido = agente.Apellido,
                Telefono = agente.Telefono,
                Correo = agente.Correo,
                CantidadPropiedades = agenteCantidadPropiedad,

            }).ToList();
            
        }
    }
}
