using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Dto.TipoVenta;
using RealStateApp.Core.Application.Interfaces.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.TipoVentas.Queries.GetAllTiposVenta
{
    public class GetAllTiposVentaQuery : IRequest<IList<TipoVentaDto>>
    {

    }

    public class GetAllTiposVentaQueryHandler : IRequestHandler<GetAllTiposVentaQuery, IList<TipoVentaDto>>
    {
        private readonly ITipoVentaRepository _tipoVentasRepository;
        private readonly IMapper _mapper;
        public GetAllTiposVentaQueryHandler(ITipoVentaRepository tipoVentasRepository, IMapper mapper)
        {
            _tipoVentasRepository = tipoVentasRepository;
            _mapper = mapper;
        }

        public async Task<IList<TipoVentaDto>> Handle(GetAllTiposVentaQuery request, CancellationToken cancellationToken)
        {
            var tiposVenta = await GetAllTiposVentas();
            if (tiposVenta == null || tiposVenta.Count == 0) throw new Exception("There is not Tipos de Ventas");
            return tiposVenta;

        }

        private async Task<List<TipoVentaDto>> GetAllTiposVentas()
        {
            var tipoVentas = await _tipoVentasRepository.GetAll();
            return tipoVentas.Select(tipoVenta => new TipoVentaDto
            {
                Id = tipoVenta.Id,
                Nombre = tipoVenta.Nombre,
                Descripcion = tipoVenta.Descripcion,
            }).ToList();
        }
    }
}
