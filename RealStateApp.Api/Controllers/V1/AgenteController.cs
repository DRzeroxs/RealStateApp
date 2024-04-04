using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealStateApp.Core.Application.Features.Agentes.Queries.ChangeStatusByAgenteId;
using RealStateApp.Core.Application.Features.Agentes.Queries.GetAgenteById;
using RealStateApp.Core.Application.Features.Agentes.Queries.GetAllAgentes;
using RealStateApp.Core.Application.Features.Agentes.Queries.GetPropiedadByAgenteId;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace RealStateApp.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [SwaggerTag("Mantenimiento de agentes")]

    public class AgenteController : BaseApiController
    {
        [HttpGet]
        [Authorize(Roles = "Admin, Developer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [SwaggerOperation(Summary = "Listado de agentes", Description = "Obtiene un listado de todos los agentes")]
        public async Task<IActionResult> List()
        {
            
            return Ok(await Mediator.Send(new GetAllAgentesQuery()));
            
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Developer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [SwaggerOperation(Summary = "Agente por id", Description = "Obtiene un agente por el id del mismo")]

        public async Task<IActionResult> GetById(int id)
        {
            
            return Ok(await Mediator.Send(new GetAgenteByIdQuery { Id = id}));
            
        }

        [HttpGet("{id}/GetPropertyByAgentId")]
        [Authorize(Roles = "Admin, Developer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [SwaggerOperation(Summary = "Propiedad por el id del agente", Description = "Obtiene una propiedad por el id del agente")]

        public async Task<IActionResult> GetAgentProperty(int id)
        {
            
            return Ok(await Mediator.Send(new GetPropiedadByAgenteIdQuery { Id = id }));
            
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [SwaggerOperation(Summary = "Cambiar estado", Description = "Recibe los parametros necesarios para cambiar el estado de un agente")]

        public async Task<IActionResult> ChangeStatus([FromBody] ChangeStatusByAgenteIdQuery request)
        {
            
            return Ok(await Mediator.Send(new ChangeStatusByAgenteIdQuery { Id = request.Id, Status = request.Status}));
            
        }
    }
}
