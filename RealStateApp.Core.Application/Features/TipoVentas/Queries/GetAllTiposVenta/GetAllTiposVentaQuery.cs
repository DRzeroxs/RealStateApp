using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Dto.TipoVenta;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.TipoVentas.Queries.GetAllTiposVenta
{
    // <summary>
    // Obtener todos los tipos de ventas
    // </summary>
    public class GetAllTiposVentaQuery : IRequest<Response<IList<TipoVentaDto>>>
    {

    }

    public class GetAllTiposVentaQueryHandler : IRequestHandler<GetAllTiposVentaQuery, Response<IList<TipoVentaDto>>>
    {
        private readonly ITipoVentaRepository _tipoVentasRepository;
        private readonly IMapper _mapper;
        public GetAllTiposVentaQueryHandler(ITipoVentaRepository tipoVentasRepository, IMapper mapper)
        {
            _tipoVentasRepository = tipoVentasRepository;
            _mapper = mapper;
        }

        public async Task<Response<IList<TipoVentaDto>>> Handle(GetAllTiposVentaQuery request, CancellationToken cancellationToken)
        {
            var tiposVenta = await _tipoVentasRepository.GetAll();
            if (tiposVenta == null || tiposVenta.Count == 0) throw new ApiExeption("No se encontrarons tipos de ventas", (int)HttpStatusCode.NotFound);
            return new Response<IList<TipoVentaDto>>(_mapper.Map<IList<TipoVentaDto>>(tiposVenta));

        }
    }
}
