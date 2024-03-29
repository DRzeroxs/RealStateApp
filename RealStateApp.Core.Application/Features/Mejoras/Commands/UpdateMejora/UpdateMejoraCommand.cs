using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.Features.Mejoras.Commands.UpdateMejora
{
    public class UpdateMejoraCommand : IRequest<MejoraUpdateResponse>
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string? Descripcion { get; set; }
    }

    public class UpdateMejoraCommandHandler : IRequestHandler<UpdateMejoraCommand, MejoraUpdateResponse>
    {
        private readonly IMejoraRepository _repository;
        private readonly IMapper _mapper;

        public UpdateMejoraCommandHandler(IMejoraRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<MejoraUpdateResponse> Handle(UpdateMejoraCommand command, CancellationToken cancellationToken)
        {
            var mejora = await _repository.GetById(command.Id);

            if (mejora == null)
            {
                throw new Exception("No se encontró la mejora");
            }

            mejora = _mapper.Map<Mejora>(command);

            await _repository.UpdateAsync(mejora, command.Id);

            return _mapper.Map<MejoraUpdateResponse>(mejora);
        }
    }
}
