using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Application.Wrappers;
using RealStateApp.Core.Domain.Entities.Descripcion;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.TipoVentas.Commands.UpdateTipoVenta
{
    // <summary>
    // Parametros para actualizar un tipo de venta
    // </summary>
    public class UpdateTipoVentaCommand : IRequest<Response<UpdateTipoVentaResponse>>
    {
        [SwaggerParameter(Description = "El id del tipo de venta que se esta actualizando")]
        public int Id { get; set; }

        [SwaggerParameter(Description = "El nuevo nombre del tipo de venta")]
        public string Nombre { get; set; }

        [SwaggerParameter(Description = "La nueva descripcion del tipo de venta")]
        public string? Descripcion { get; set; }
    }

    public class UpdateTipoVentaCommandHandler : IRequestHandler<UpdateTipoVentaCommand, Response<UpdateTipoVentaResponse>>
    {
        private readonly ITipoVentaRepository _tipoVentaRepository;
        private readonly IMapper _mapper;

        public UpdateTipoVentaCommandHandler(ITipoVentaRepository tipoVentaRepository, IMapper mapper)
        {
            _tipoVentaRepository = tipoVentaRepository;
            _mapper = mapper;
        }

        public async Task<Response<UpdateTipoVentaResponse>> Handle(UpdateTipoVentaCommand command, CancellationToken cancellationToken)
        {
            var tipoVenta = await _tipoVentaRepository.GetById(command.Id);
            if (tipoVenta == null) throw new ApiExeption("El tipo de venta no fue encontrada", (int)HttpStatusCode.NotFound);
            tipoVenta = _mapper.Map<TipoVenta>(command);
            await _tipoVentaRepository.UpdateAsync(tipoVenta, tipoVenta.Id);
            var tipoVentaResponse = _mapper.Map<UpdateTipoVentaResponse>(tipoVenta);
            return new Response<UpdateTipoVentaResponse>(tipoVentaResponse);
        }
    }
}
