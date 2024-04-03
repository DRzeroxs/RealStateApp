using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace RealStateApp.Core.Application.Features.Mejoras.Commands.DeleteMejoraById
{
    //<summary>
    // Command para eliminar una mejora por Id
    //</summary>
    public class DeleteMejoraByIdCommand : IRequest<Response<int>>
    {
        //<example>
        // 1
        //</example>
        [SwaggerParameter(Description = "Identificador (int) de la mejora a eliminar")]
        public int Id { get; set; }
    }

    public class DeleteMejoraByIdCommandHandler : IRequestHandler<DeleteMejoraByIdCommand, Response<int>>
    {
        private readonly IMejoraRepository _repository;
        private readonly IMapper _mapper;

        public DeleteMejoraByIdCommandHandler(IMejoraRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(DeleteMejoraByIdCommand command, CancellationToken cancellationToken)
        {
            var mejora = await _repository.GetById(command.Id);

            if (mejora == null)
            {
                throw new ApiEception("No se encontró la mejora",(int)HttpStatusCode.NotFound);
            }

            await _repository.DeleteAsync(mejora);

            return new Response<int>(mejora.Id);
        }
    }
}
