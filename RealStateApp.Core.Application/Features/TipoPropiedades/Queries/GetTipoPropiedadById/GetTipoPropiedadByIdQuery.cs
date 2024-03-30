using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Dto.TipoDePropiedad;
using RealStateApp.Core.Application.Interfaces.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.TipoPropiedades.Queries.GetTipoPropiedadById
{
    public class GetTipoPropiedadByIdQuery : IRequest<TipoPropiedadDto>
    {
        public int Id { get; set; }
    }

    public class GetTipoPropiedadByIdQueryHandler : IRequestHandler<GetTipoPropiedadByIdQuery, TipoPropiedadDto>
    {
        private readonly ITipoPropiedadRepository _tipoPropiedadRepository;
        private readonly IMapper _mapper;
        public GetTipoPropiedadByIdQueryHandler(ITipoPropiedadRepository tipoPropiedadRepository, IMapper mapper)
        {
            _tipoPropiedadRepository = tipoPropiedadRepository;
            _mapper = mapper;
        }
        public async Task<TipoPropiedadDto> Handle(GetTipoPropiedadByIdQuery request, CancellationToken cancellationToken)
        {
            var tipoPropiedad = await GetTipoPropiedadById(request.Id);
            if (tipoPropiedad == null) throw new Exception("Tipo propiedad not found");
            return tipoPropiedad;
        }

        public async Task<TipoPropiedadDto> GetTipoPropiedadById(int id)
        {
            var tipoPropiedad = await _tipoPropiedadRepository.GetById(id);
            TipoPropiedadDto tipoPropiedadDto = new TipoPropiedadDto()
            {
                Id = tipoPropiedad.Id,
                Nombre = tipoPropiedad.Nombre,
                Descripcion = tipoPropiedad.Descripcion
            };

            return tipoPropiedadDto;
        }
    }
}
