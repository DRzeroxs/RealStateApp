using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Domain.Entities.Descripcion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.TipoPropiedades.Commands.CreateTipoPropiedad
{
    public class CreateTipoPropiedadCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }

    public class CreateTipoPropiedadCommandHandler : IRequestHandler<CreateTipoPropiedadCommand, int>
    {
        private readonly ITipoPropiedadRepository _tipoPropiedadRepository;
        private readonly IMapper _mapper;
        public CreateTipoPropiedadCommandHandler(ITipoPropiedadRepository tipoPropiedadRepository, IMapper mapper)
        {
            _tipoPropiedadRepository = tipoPropiedadRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateTipoPropiedadCommand command, CancellationToken cancellationToken)
        {
            var tipoPropiedad = _mapper.Map<TipoPropiedad>(command);
            if (tipoPropiedad == null) throw new Exception("Tipo de propiedad not found");
            tipoPropiedad = await _tipoPropiedadRepository.AddAsync(tipoPropiedad);
            return tipoPropiedad.Id;
        }
    }
}
