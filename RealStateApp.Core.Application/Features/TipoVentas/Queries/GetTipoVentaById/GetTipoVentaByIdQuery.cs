using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Dto.TipoVenta;
using RealStateApp.Core.Application.Interfaces.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.TipoVentas.Queries.GetTipoVentaById
{
    public class GetTipoVentaByIdQuery : IRequest<TipoVentaDto>
    {
        public int Id { get; set; }
    }

    public class GetTipoVentaByIdQueryHandler : IRequestHandler<GetTipoVentaByIdQuery, TipoVentaDto>
    {
        private readonly ITipoVentaRepository _tipoVentaRepository;
        private readonly IMapper _mapper;

        public GetTipoVentaByIdQueryHandler(ITipoVentaRepository tipoVentaRepository, IMapper mapper)
        {
            _tipoVentaRepository = tipoVentaRepository;
            _mapper = mapper;
        }

        public async Task<TipoVentaDto> Handle(GetTipoVentaByIdQuery request, CancellationToken cancellationToken)
        {
            var tipoVenta = await GetTipoVentaById(request.Id);
            if (tipoVenta == null) throw new Exception("Tipo venta was not found");
            return tipoVenta;
        }

        private async Task<TipoVentaDto> GetTipoVentaById(int id)
        {
            var tipoVenta = await _tipoVentaRepository.GetById(id);
            TipoVentaDto tipoVentaDto = new TipoVentaDto()
            {
                Id = id,
                Nombre = tipoVenta.Nombre,
                Descripcion = tipoVenta.Descripcion
            };
            return tipoVentaDto;
        }
    }
}
