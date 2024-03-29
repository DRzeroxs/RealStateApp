using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.Mejoras.Commands.CreateMejora
{
    public class CreateMejoraCommand : IRequest<int>
    {
        public string Nombre { get; set; }
        public string? Descripcion { get; set; }
    }

    public class CreateMejoraCommandHandler : IRequestHandler<CreateMejoraCommand, int>
    {
        private readonly IMejoraRepository _repository;
        private readonly IMapper _mapper;

        public CreateMejoraCommandHandler(IMejoraRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateMejoraCommand command, CancellationToken cancellationToken)
        {
            var mejora = _mapper.Map<Mejora>(command);

            var newMejora = await _repository.AddAsync(mejora);

            return newMejora.Id;
        }
    }
}
