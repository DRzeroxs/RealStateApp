using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Dto.Mejora;
using RealStateApp.Core.Application.Dto.TipoVenta;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Application.Wrappers;
using RealStateApp.Core.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.TipoVentas.Queries.GetTipoVentaById
{
    // <summary>
    // Parametros para obtener un tipo de venta por el id
    // </summary>
    public class GetTipoVentaByIdQuery : IRequest<Response<TipoVentaDto>>
    {
        [SwaggerParameter(Description = "Id del tipo de venta que desea obtener")]
        public int Id { get; set; }
    }

    public class GetTipoVentaByIdQueryHandler : IRequestHandler<GetTipoVentaByIdQuery, Response<TipoVentaDto>>
    {
        private readonly ITipoVentaRepository _tipoVentaRepository;
        private readonly IMapper _mapper;

        public GetTipoVentaByIdQueryHandler(ITipoVentaRepository tipoVentaRepository, IMapper mapper)
        {
            _tipoVentaRepository = tipoVentaRepository;
            _mapper = mapper;
        }

        public async Task<Response<TipoVentaDto>> Handle(GetTipoVentaByIdQuery request, CancellationToken cancellationToken)
        {
            var tipoVenta = await _tipoVentaRepository.GetById(request.Id);
            if (tipoVenta == null) throw new ApiExeption("No se encontro un tipo de venta por ese id", (int)HttpStatusCode.NotFound);
            return new Response<TipoVentaDto>(_mapper.Map<TipoVentaDto>(tipoVenta));
        }
    }
}
