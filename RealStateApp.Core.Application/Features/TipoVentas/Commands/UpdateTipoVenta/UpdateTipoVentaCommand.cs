using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Domain.Entities.Descripcion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.TipoVentas.Commands.UpdateTipoVenta
{
    public class UpdateTipoVentaCommand : IRequest<UpdateTipoVentaResponse>
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string? Descripcion { get; set; }
    }

    public class UpdateTipoVentaCommandHandler : IRequestHandler<UpdateTipoVentaCommand, UpdateTipoVentaResponse>
    {
        private readonly ITipoVentaRepository _tipoVentaRepository;
        private readonly IMapper _mapper;

        public UpdateTipoVentaCommandHandler(ITipoVentaRepository tipoVentaRepository, IMapper mapper)
        {
            _tipoVentaRepository = tipoVentaRepository;
            _mapper = mapper;
        }

        public async Task<UpdateTipoVentaResponse> Handle(UpdateTipoVentaCommand command, CancellationToken cancellationToken)
        {
            var tipoVenta = await _tipoVentaRepository.GetById(command.Id);
            if (tipoVenta == null) throw new Exception("Tipo Venta was not found");
            tipoVenta = _mapper.Map<TipoVenta>(command);
            await _tipoVentaRepository.UpdateAsync(tipoVenta, tipoVenta.Id);
            var tipoVentaResponse = _mapper.Map<UpdateTipoVentaResponse>(tipoVenta);
            return tipoVentaResponse;
        }
    }
}
