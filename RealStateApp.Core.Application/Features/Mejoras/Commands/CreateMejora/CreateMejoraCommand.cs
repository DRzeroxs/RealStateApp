using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Application.Wrappers;
using RealStateApp.Core.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.Mejoras.Commands.CreateMejora
{
    //<summary>
    //Clase con los parametros para crear una mejora
    //</summary>
    public class CreateMejoraCommand : IRequest<Response<int>>
    {
        //<example>
        //Incluir Piscina
        //</example>
        [SwaggerParameter(Description = "Nombre de la mejora")]
        public string Nombre { get; set; }

        //<example>
        //Incluye el acceso a la piscina
        //</example>
        [SwaggerParameter(Description = "Descripcion de la mejora")]
        public string? Descripcion { get; set; }
    }

    public class CreateMejoraCommandHandler : IRequestHandler<CreateMejoraCommand, Response<int>>
    {
        private readonly IMejoraRepository _repository;
        private readonly IMapper _mapper;

        public CreateMejoraCommandHandler(IMejoraRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateMejoraCommand command, CancellationToken cancellationToken)
        {
            var mejora = _mapper.Map<Mejora>(command);

            var newMejora = await _repository.AddAsync(mejora);

            return new Response<int>(newMejora.Id);
        }
    }
}
