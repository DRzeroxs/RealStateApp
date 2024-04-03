using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Dto.Agente;
using RealStateApp.Core.Application.Interfaces.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.Agentes.Queries.GetAllAgentes
{
    public class GetAllAgentesQuery : IRequest<IList<AgenteDto>>
    {
    }
    public class GetAllAgentesQueryHandler : IRequestHandler<GetAllAgentesQuery, IList<AgenteDto>>
    {
        private readonly IAgenteRepository _agenteRepository;
        public GetAllAgentesQueryHandler(IAgenteRepository agenteRepository)
        {
            _agenteRepository = agenteRepository;
        }

        public async Task<IList<AgenteDto>> Handle(GetAllAgentesQuery request, CancellationToken cancellationToken)
        {
            var agentes = await GetAllAgentes();
            if (agentes == null || agentes.Count == 0) throw new Exception("There is not agentes");
            return agentes;
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
                CantidadPropiedades = agenteCantidadPropiedad,

            }).ToList();
            
        }
    }
}
