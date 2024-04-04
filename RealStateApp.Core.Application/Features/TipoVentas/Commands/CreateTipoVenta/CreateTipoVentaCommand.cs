using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Application.Wrappers;
using RealStateApp.Core.Domain.Entities.Descripcion;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.TipoVentas.Commands.CreateTipoVenta
{
    // <summary>
    // Parametros para crear un tipo de venta
    // </summary>
    public class CreateTipoVentaCommand : IRequest<Response<int>>
    {

        // <summary>
        // Ventas de propiedades en remate
        // </summary>

        [SwaggerParameter(Description = "Nombre del tipo de venta")]
        public string Nombre { get; set; }

        // <summary>
        // Propiedades que se venden para pagar una deuda hipotecaria
        // </summary>

        [SwaggerParameter(Description = "Descripcion del tipo de venta")]
        public string? Descripcion { get; set; }
    }

    public class CreateTipoVentaCommandHandler : IRequestHandler<CreateTipoVentaCommand, Response<int>>
    {
        private readonly ITipoVentaRepository _tipoVentaRepository;
        private readonly IMapper _mapper;
        public CreateTipoVentaCommandHandler(ITipoVentaRepository tipoVentaRepository, IMapper mapper)
        {
            _tipoVentaRepository = tipoVentaRepository;
            _mapper = mapper;
        }
        
        public async Task<Response<int>> Handle(CreateTipoVentaCommand command, CancellationToken cancellationToken)
        {
            var tipoVenta = _mapper.Map<TipoVenta>(command);
            tipoVenta = await _tipoVentaRepository.AddAsync(tipoVenta);
            return new Response<int>(tipoVenta.Id);
        }
    }
}
