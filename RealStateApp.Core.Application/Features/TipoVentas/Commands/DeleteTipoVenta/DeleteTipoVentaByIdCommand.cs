using MediatR;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.TipoVentas.Commands.DeleteTipoVenta
{
    // <summary>
    // Parametros para eliminar un tipo de venta
    // </summary>
    public class DeleteTipoVentaByIdCommand : IRequest<Response<int>>
    {
        [SwaggerParameter(Description = "El id del tipo de venta que se desea eliminar")]
        public int Id { get; set; }
    }
    public class DeleteTipoVentaByIdCommandHandler : IRequestHandler<DeleteTipoVentaByIdCommand, Response<int>>
    {
        private readonly ITipoVentaRepository _tipoVentaRepository;
        public DeleteTipoVentaByIdCommandHandler(ITipoVentaRepository tipoVentaRepository)
        {
            _tipoVentaRepository = tipoVentaRepository;
        }

        public async Task<Response<int>> Handle(DeleteTipoVentaByIdCommand command, CancellationToken cancellationToken)
        {
            var tipoVenta = await _tipoVentaRepository.GetById(command.Id);
            if (tipoVenta == null) throw new ApiExeption("El tipo de venta no fue encontrada", (int)HttpStatusCode.NotFound);
            await _tipoVentaRepository.DeleteAsync(tipoVenta);
            return new Response<int>(tipoVenta.Id);
        }
    }
}
