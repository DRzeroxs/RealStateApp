using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Interfaces.IRepository;

namespace RealStateApp.Core.Application.Features.Mejoras.Commands.DeleteMejoraById
{
    public class DeleteMejoraByIdCommand : IRequest<int>
    {
        public int Id { get; set; }
    }

    public class DeleteMejoraByIdCommandHandler : IRequestHandler<DeleteMejoraByIdCommand, int>
    {
        private readonly IMejoraRepository _repository;
        private readonly IMapper _mapper;

        public DeleteMejoraByIdCommandHandler(IMejoraRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> Handle(DeleteMejoraByIdCommand command, CancellationToken cancellationToken)
        {
            var mejora = await _repository.GetById(command.Id);

            if (mejora == null)
            {
                throw new Exception("No se encontró la mejora");
            }

            await _repository.DeleteAsync(mejora);

            return mejora.Id;
        }
    }
}
