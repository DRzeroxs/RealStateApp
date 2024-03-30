using MediatR;
using RealStateApp.Core.Application.Interfaces.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.TipoVentas.Commands.DeleteTipoVenta
{
    public class DeleteTipoVentaByIdCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
    public class DeleteTipoVentaByIdCommandHandler : IRequestHandler<DeleteTipoVentaByIdCommand, int>
    {
        private readonly ITipoVentaRepository _tipoVentaRepository;
        public DeleteTipoVentaByIdCommandHandler(ITipoVentaRepository tipoVentaRepository)
        {
            _tipoVentaRepository = tipoVentaRepository;
        }

        public async Task<int> Handle(DeleteTipoVentaByIdCommand command, CancellationToken cancellationToken)
        {
            var tipoVenta = await _tipoVentaRepository.GetById(command.Id);
            if (tipoVenta == null) throw new Exception("Tipo venta was not found");
            await _tipoVentaRepository.DeleteAsync(tipoVenta);
            return tipoVenta.Id;
        }
    }
}
