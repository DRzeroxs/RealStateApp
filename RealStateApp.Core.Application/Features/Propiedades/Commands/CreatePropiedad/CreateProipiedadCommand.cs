using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.Propiedades.Commands.CreatePropiedad
{
    public class CreateProipiedadCommand: IRequest<int>
    {
        public int Id { get; set; }
        public string? Identifier { get; set; }

        public string Size { get; set; }

        public double Precio { get; set; }

        public string Descripcion { get; set; }

        public int NumHabitaciones { get; set; }

        public int NumAceados { get; set; }

        public int TipoPropiedadId { get; set; }

        public int TipoVentaId { get; set; }

        public int AgenteId { get; set; }
    }
    public class CreatePropiedadCommandHandler : IRequestHandler<CreateProipiedadCommand, int>
    {
        private readonly IPropiedadRepository _repository;
        private readonly IMapper _mapper;
        public CreatePropiedadCommandHandler(IPropiedadRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateProipiedadCommand command, CancellationToken cancellationToken)
        {
            var propiedad = _mapper.Map<Propiedad>(command);

            await _repository.AddAsync(propiedad);  

            return propiedad.Id;
        }
    }
}
