using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Domain.Entities.Descripcion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.TipoPropiedades.Commands.UpdateTipoPropiedad
{
    public class UpdateTipoPropiedadCommand : IRequest<UpdateTipoPropiedadResponse>
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string? Descripcion { get; set; }
    }

    public class UpdateTipoPropiedadCommandHandler : IRequestHandler<UpdateTipoPropiedadCommand, UpdateTipoPropiedadResponse>
    {
        private readonly ITipoPropiedadRepository _tipoPropiedadRepository;
        private readonly IMapper _mapper;
        public UpdateTipoPropiedadCommandHandler(ITipoPropiedadRepository tipoPropiedadRepository, IMapper mapper)
        {
            _tipoPropiedadRepository = tipoPropiedadRepository;
            _mapper = mapper;
        }

        public async Task<UpdateTipoPropiedadResponse> Handle(UpdateTipoPropiedadCommand command, CancellationToken cancellationToken)
        {
            var tipoPropiedad = await _tipoPropiedadRepository.GetById(command.Id);
            if (tipoPropiedad == null) throw new Exception("Tipo propiedad not found");
            tipoPropiedad = _mapper.Map<TipoPropiedad>(command);
            await _tipoPropiedadRepository.UpdateAsync(tipoPropiedad, tipoPropiedad.Id);
            var tipoPropiedadResponse = _mapper.Map<UpdateTipoPropiedadResponse>(tipoPropiedad);
            return tipoPropiedadResponse;
        }
    }
}
