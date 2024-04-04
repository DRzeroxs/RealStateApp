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

namespace RealStateApp.Core.Application.Features.TipoPropiedades.Commands.CreateTipoPropiedad
{
    // <summary>
    // Parametros para crear una tipo de propiedad
    // </summary>
    public class CreateTipoPropiedadCommand : IRequest<Response<int>>
    {
        // <summary>
        // Condominio / Apartamentos
        // </summary>

        [SwaggerParameter(Description = "Nombre del tipo de propiedad")]
        public string Nombre { get; set; }

        // <summary>
        // Son viviendas ubicadas en edificios con multiples pisos
        // </summary>

        [SwaggerParameter(Description = "Descripcion del tipo de propiedad")]
        public string Descripcion { get; set; }
    }

    public class CreateTipoPropiedadCommandHandler : IRequestHandler<CreateTipoPropiedadCommand, Response<int>>
    {
        private readonly ITipoPropiedadRepository _tipoPropiedadRepository;
        private readonly IMapper _mapper;
        public CreateTipoPropiedadCommandHandler(ITipoPropiedadRepository tipoPropiedadRepository, IMapper mapper)
        {
            _tipoPropiedadRepository = tipoPropiedadRepository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateTipoPropiedadCommand command, CancellationToken cancellationToken)
        {
            var tipoPropiedad = _mapper.Map<TipoPropiedad>(command);
            tipoPropiedad = await _tipoPropiedadRepository.AddAsync(tipoPropiedad);
            return new Response<int>(tipoPropiedad.Id);
        }
    }
}
