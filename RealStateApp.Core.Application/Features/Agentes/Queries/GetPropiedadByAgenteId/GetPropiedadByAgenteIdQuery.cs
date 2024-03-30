using MediatR;
using RealStateApp.Core.Application.Dto.Agente;
using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Application.ViewModel.TipoPropiedad;
using RealStateApp.Core.Application.ViewModel.TipoVenta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.Agentes.Queries.GetPropiedadByAgenteId
{
    public class GetPropiedadByAgenteIdQuery : IRequest<PropiedadByAgenteDto>
    {
        public int Id { get; set; }
    }

    public class GetPropiedadByAgenteIdQueryHandler : IRequestHandler<GetPropiedadByAgenteIdQuery, PropiedadByAgenteDto>
    {
        private readonly IAgenteRepository _agenteRepository;
        public GetPropiedadByAgenteIdQueryHandler(IAgenteRepository agenteRepository)
        {
            _agenteRepository = agenteRepository;
        }
        public async Task<PropiedadByAgenteDto> Handle(GetPropiedadByAgenteIdQuery request, CancellationToken cancellationToken)
        {
            var propiedadByAgente = await GetPropiedadByAgenteId(request.Id);
            if (propiedadByAgente == null) throw new Exception("Agente doesn't have propiedad");
            return propiedadByAgente;
        }

        private async Task<PropiedadByAgenteDto> GetPropiedadByAgenteId(int id)
        {
            var propiedadByAgente = await _agenteRepository.GetPropiedadByAgenteId(id);
            PropiedadByAgenteDto propiedadByAgenteDto = new PropiedadByAgenteDto();
            propiedadByAgenteDto.Id = id;
            propiedadByAgenteDto.Identifier = propiedadByAgente.Propiedad.Identifier;
            propiedadByAgenteDto.Precio = propiedadByAgente.Propiedad.Precio;
            propiedadByAgenteDto.Size = propiedadByAgente.Propiedad.Size;
            propiedadByAgenteDto.NumAceados = propiedadByAgente.Propiedad.NumAceados;
            propiedadByAgenteDto.NumHabitaciones = propiedadByAgente.Propiedad.NumHabitaciones;
            propiedadByAgenteDto.Descripcion = propiedadByAgente.Propiedad.Descripcion;

            propiedadByAgenteDto.TipoPropiedad = new TipoPropiedadViewModel
            {
                Id = propiedadByAgente.Propiedad.TipoPropiedad.Id,
                Nombre = propiedadByAgente.Propiedad.TipoPropiedad.Nombre,
                Descripcion = propiedadByAgente.Propiedad.TipoPropiedad.Descripcion
            };

            propiedadByAgenteDto.TipoVenta = new TipoVentaViewModel
            {
                Id = propiedadByAgente.Propiedad.TipoVenta.Id,
                Nombre = propiedadByAgente.Propiedad.TipoVenta.Nombre,
                Descripcion = propiedadByAgente.Propiedad.TipoVenta.Descripcion,
            };
            

            return propiedadByAgenteDto;
        }
    }
}
