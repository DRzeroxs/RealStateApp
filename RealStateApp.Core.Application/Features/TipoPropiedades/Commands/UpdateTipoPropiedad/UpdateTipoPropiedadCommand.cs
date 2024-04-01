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

namespace RealStateApp.Core.Application.Features.TipoPropiedades.Commands.UpdateTipoPropiedad
{
    // <summary>
    // Parametros para actualizar un tipo de propiedad
    // </summary>
    public class UpdateTipoPropiedadCommand : IRequest<Response<UpdateTipoPropiedadResponse>>
    {
        [SwaggerParameter(Description = "El id del tipo de propiedad que se esta actualizando")]
        public int Id { get; set; }

        [SwaggerParameter(Description = "El nuevo nombre del tipo de propiedad")]
        public string Nombre { get; set; }

        [SwaggerParameter(Description = "La nueva descripcion del tipo de propiedad")]
        public string? Descripcion { get; set; }
    }

    public class UpdateTipoPropiedadCommandHandler : IRequestHandler<UpdateTipoPropiedadCommand, Response<UpdateTipoPropiedadResponse>>
    {
        private readonly ITipoPropiedadRepository _tipoPropiedadRepository;
        private readonly IMapper _mapper;
        public UpdateTipoPropiedadCommandHandler(ITipoPropiedadRepository tipoPropiedadRepository, IMapper mapper)
        {
            _tipoPropiedadRepository = tipoPropiedadRepository;
            _mapper = mapper;
        }

        public async Task<Response<UpdateTipoPropiedadResponse>> Handle(UpdateTipoPropiedadCommand command, CancellationToken cancellationToken)
        {
            var tipoPropiedad = await _tipoPropiedadRepository.GetById(command.Id);

            if (tipoPropiedad == null) throw new ApiExeption("El tipo de propiedad no fue encontrada", (int)HttpStatusCode.NotFound);

            tipoPropiedad = _mapper.Map<TipoPropiedad>(command);

            await _tipoPropiedadRepository.UpdateAsync(tipoPropiedad, tipoPropiedad.Id);

            var tipoPropiedadResponse = _mapper.Map<UpdateTipoPropiedadResponse>(tipoPropiedad);

            return new Response<UpdateTipoPropiedadResponse>(tipoPropiedadResponse);
        }
    }
}
