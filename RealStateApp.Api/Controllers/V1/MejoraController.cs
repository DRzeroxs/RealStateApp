using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Dto.Mejora;
using RealStateApp.Core.Application.Features.Mejoras.Commands.CreateMejora;
using RealStateApp.Core.Application.Features.Mejoras.Commands.DeleteMejoraById;
using RealStateApp.Core.Application.Features.Mejoras.Commands.UpdateMejora;
using RealStateApp.Core.Application.Features.Mejoras.Queries.GetAllMejoras;
using RealStateApp.Core.Application.Features.Mejoras.Queries.GetMejoraById;
using RealStateApp.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace RealStateApp.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Authorize(Roles = "Admin, Developer")]
    [SwaggerTag("CRUD de Mejora")]
    public class MejoraController : BaseApiController
    {

        [Authorize(Roles = "Admin, Developer")]
        [HttpGet("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MejoraDto))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Obtener una mejora por Id",
            Description = "Obtiene una mejora por Id tipo int"
        )]
        public async Task<IActionResult> GetById(int id)
        {

            return Ok(await Mediator.Send(new GetMejoraByIdQuery { Id = id }));

        }

        [Authorize(Roles = "Admin, Developer")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<MejoraDto>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Lista de mejoras",
            Description = "Obtiene una lista de Dtos de todas las mejoras registradas"
        )]
        public async Task<IActionResult> List()
        {

            return Ok(await Mediator.Send(new GetAllMejoraQuery { }));


        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(MejoraDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Creacion de Mejora",
            Description = "Recibe los parametros (Nombre y Descripcion) para registrar una mejora"
        )]
        public async Task<IActionResult> Create([FromBody] CreateMejoraCommand command)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Response<int> result = await Mediator.Send(command);

            int createdId = result.Data;

            Response<MejoraDto> mejoraCreated = await Mediator.Send(new GetMejoraByIdQuery { Id = createdId });

            if (mejoraCreated != null)
            {
                return CreatedAtAction(nameof(GetById), new { id = createdId }, mejoraCreated);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al recuperar la mejora creada");
            }

        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Actualiza una Mejora",
            Description = "Recibe los parametros (Id, Nombre y Descripcion) para Actualizar una mejora"
        )]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateMejoraCommand command)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != command.Id)
            {
                return BadRequest("El ID en la Url no coincide con el Id en el Objeto de comando.");
            }

            return Ok(await Mediator.Send(command));


        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Elimina una Mejora",
            Description = "Recibe el Id (int) de una mejora para eliminarla"
        )]
        public async Task<IActionResult> Delete(int id)
        {

            await Mediator.Send(new DeleteMejoraByIdCommand { Id = id });

            return NoContent();

        }


    }
}
