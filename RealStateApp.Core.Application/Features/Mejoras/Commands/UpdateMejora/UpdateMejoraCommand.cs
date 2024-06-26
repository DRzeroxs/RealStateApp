﻿using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Application.Wrappers;
using RealStateApp.Core.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace RealStateApp.Core.Application.Features.Mejoras.Commands.UpdateMejora
{
    //<summary>
    // Command para actualizar una mejora
    //</summary>
    public class UpdateMejoraCommand : IRequest<Response<MejoraUpdateResponse>>
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

    public class UpdateMejoraCommandHandler : IRequestHandler<UpdateMejoraCommand, Response<MejoraUpdateResponse>>
    {
        private readonly IMejoraRepository _repository;
        private readonly IMapper _mapper;

        public UpdateMejoraCommandHandler(IMejoraRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<MejoraUpdateResponse>> Handle(UpdateMejoraCommand command, CancellationToken cancellationToken)
        {
            var mejora = await _repository.GetById(command.Id);

            if (mejora == null)
            {
                throw new ApiExeption("No se encontró la mejora", (int)HttpStatusCode.NotFound);
            }

            mejora = _mapper.Map<Mejora>(command);

            await _repository.UpdateAsync(mejora, command.Id);

            return new Response<MejoraUpdateResponse> (_mapper.Map<MejoraUpdateResponse>(mejora));
        }
    }
}
