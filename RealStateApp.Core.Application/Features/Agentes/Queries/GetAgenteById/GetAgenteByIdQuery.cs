using MediatR;
using RealStateApp.Core.Application.Dto.Agente;
using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.Agentes.Queries.GetAgenteById
{
    public class GetAgenteByIdQuery : IRequest<AgenteDto>
    {
        public int Id { get; set; }
    }

    public class GetAgenteByIdQueryHandler : IRequestHandler<GetAgenteByIdQuery, AgenteDto>
    {
        private readonly IAgenteRepository _agenteRepository;
        public GetAgenteByIdQueryHandler(IAgenteRepository agenteRepository)
        {
            _agenteRepository = agenteRepository;
        }
        public async Task<AgenteDto> Handle(GetAgenteByIdQuery request, CancellationToken cancellationToken)
        {
            var agente = await GetById(request.Id);
            if (agente == null) throw new Exception("There is not Agente");
            return agente;
        }

        private async Task<AgenteDto> GetById(int id)
        { 
            var agente = await _agenteRepository.GetById(id);
            var agenteCantidadPropoiedad = await _agenteRepository.GetCantidadPropiedadAgenteById(id);
            AgenteDto agenteDto = new AgenteDto()
            {
                Id = id,
                Nombre = agente.Nombre,
                Apellido = agente.Apellido,
                Telefono = agente.Telefono,
                CantidadPropiedades = agenteCantidadPropoiedad
            };
            return agenteDto;
        }
    }
}
