using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Domain.Entities.Descripcion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.TipoVentas.Commands.CreateTipoVenta
{
    public class CreateTipoVentaCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string? Descripcion { get; set; }
    }

    public class CreateTipoVentaCommandHandler : IRequestHandler<CreateTipoVentaCommand, int>
    {
        private readonly ITipoVentaRepository _tipoVentaRepository;
        private readonly IMapper _mapper;
        public CreateTipoVentaCommandHandler(ITipoVentaRepository tipoVentaRepository, IMapper mapper)
        {
            _tipoVentaRepository = tipoVentaRepository;
            _mapper = mapper;
        }
        
        public async Task<int> Handle(CreateTipoVentaCommand command, CancellationToken cancellationToken)
        {
            var tipoVenta = _mapper.Map<TipoVenta>(command);
            if (tipoVenta == null) throw new Exception("Tipo de propiedad not found");
            tipoVenta = await _tipoVentaRepository.AddAsync(tipoVenta);
            return tipoVenta.Id;
        }
    }
}
