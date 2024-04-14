using MediatR;
using RealStateApp.Core.Application.Dto.Agente;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Application.ViewModel.Mejora;
using RealStateApp.Core.Application.ViewModel.TipoPropiedad;
using RealStateApp.Core.Application.ViewModel.TipoVenta;
using RealStateApp.Core.Application.Wrappers;
using RealStateApp.Core.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.Agentes.Queries.GetPropiedadByAgenteId
{
    // <summary>
    // Obtener una propiedad del agente por el id
    // </summary>
    public class GetPropiedadByAgenteIdQuery : IRequest<Response<PropiedadByAgenteDto>>
    {
        [SwaggerParameter(Description = "Id del agente que desea obtener")]
        public int Id { get; set; }
    }

    public class GetPropiedadByAgenteIdQueryHandler : IRequestHandler<GetPropiedadByAgenteIdQuery, Response<PropiedadByAgenteDto>>
    {
        private readonly IAgenteRepository _agenteRepository;
        private readonly IMejoraRepository _mejoraRepository;
        public GetPropiedadByAgenteIdQueryHandler(IAgenteRepository agenteRepository, IMejoraRepository mejoraRepository)
        {
            _agenteRepository = agenteRepository;
            _mejoraRepository = mejoraRepository;
        }
        public async Task<Response<PropiedadByAgenteDto>> Handle(GetPropiedadByAgenteIdQuery request, CancellationToken cancellationToken)
        {
            var propiedadByAgente = await GetPropiedadByAgenteId(request.Id);
            if (propiedadByAgente == null) throw new ApiExeption("Ese agente no tiene propiedad", (int)HttpStatusCode.NotFound);
            return new Response<PropiedadByAgenteDto>(propiedadByAgente);
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

            //propiedadByAgenteDto.Mejora.Select(x => new MejoraViewModel
            //{
            //    Id = propiedadByAgente.Propiedad.MejorasAplicadas.Mejora.Id,
            //    Nombre = propiedadByAgente.Propiedad.MejorasAplicadas.Mejora.Nombre,
            //    Descripcion = propiedadByAgente.Propiedad.MejorasAplicadas.Mejora.Descripcion
            //}).ToList();

            var mejora = await _mejoraRepository.GetAll();
            propiedadByAgenteDto.Mejora = mejora.Select(x => new MejoraViewModel
            {
                Id = propiedadByAgente.Propiedad.MejorasAplicadas.Mejora.Id,
                Nombre = propiedadByAgente.Propiedad.MejorasAplicadas.Mejora.Nombre,
                Descripcion = propiedadByAgente.Propiedad.MejorasAplicadas.Mejora.Descripcion

            }).ToList();

            return propiedadByAgenteDto;
        }
    }
}
