using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace RealStateApp.Core.Application.Features.Mejoras.Commands.UpdateMejora
{
    //<summary>
    // Command para actualizar una mejora
    //</summary>
    public class UpdateMejoraCommand : IRequest<MejoraUpdateResponse>
    {
        // <example>
        // 1
        // </example>
        [SwaggerParameter(Description = "Identificador (int) de la mejora")]
        public int Id { get; set; }

        // <example>
        // Incluir Piscina
        // </example>
        [SwaggerParameter(Description = "Nombre de la mejora")]
        public string Nombre { get; set; }

        // <example>
        // Incluye el acceso a la piscina
        // </example>
        [SwaggerParameter(Description = "Descripcion de la mejora")]
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
