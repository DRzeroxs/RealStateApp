using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.TipoPropiedades.Commands.DeleteTipoPropiedadById
{
    // <summary>
    // Parametros para eliminar un tipo de propiedad
    // </summary>
    public class DeleteTipoPropiedadByIdCommand : IRequest<Response<int>>
    {
        [SwaggerParameter(Description = "El id del tipo de propiedad que se desea eliminar")]
        public int Id { get; set; }
    }

    public class DeleteTipoPropiedadByIdCommandHandler : IRequestHandler<DeleteTipoPropiedadByIdCommand, Response<int>>
    {
        private readonly ITipoPropiedadRepository _tipoPropiedadRepository;
        public DeleteTipoPropiedadByIdCommandHandler(ITipoPropiedadRepository tipoPropiedadRepository)
        {
            _tipoPropiedadRepository = tipoPropiedadRepository;
        }

        public async Task<Response<int>> Handle(DeleteTipoPropiedadByIdCommand command, CancellationToken cancellationToken)
        {
            var tipoPropiedad = await _tipoPropiedadRepository.GetById(command.Id);
            if (tipoPropiedad == null) throw new ApiExeption("El tipo de propiedad no fue encontrada", (int)HttpStatusCode.NotFound);
            await _tipoPropiedadRepository.DeleteAsync(tipoPropiedad);
            return new Response<int>(tipoPropiedad.Id);
        }
    }
}
