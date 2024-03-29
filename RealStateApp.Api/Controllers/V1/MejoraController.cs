using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Dto.Mejora;
using RealStateApp.Core.Application.Features.Mejoras.Commands.CreateMejora;
using RealStateApp.Core.Application.Features.Mejoras.Commands.DeleteMejoraById;
using RealStateApp.Core.Application.Features.Mejoras.Commands.UpdateMejora;
using RealStateApp.Core.Application.Features.Mejoras.Queries.GetAllMejoras;
using RealStateApp.Core.Application.Features.Mejoras.Queries.GetMejoraById;
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
            try
            {
                return Ok(await Mediator.Send(new GetMejoraByIdQuery { Id = id }));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }


        }

        [Authorize(Roles = "Admin, Developer")]
        [HttpGet("{id}")]
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
            try
            {
                return Ok(await Mediator.Send(new GetAllMejoraQuery { }));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }


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
        public async Task<IActionResult> Create([FromBody]CreateMejoraCommand command)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                int resultId = await Mediator.Send(command);

                MejoraDto mejoraCreated = await Mediator.Send(new GetMejoraByIdQuery { Id = resultId });

                if (mejoraCreated != null)
                {
                    return CreatedAtAction(nameof(GetById), new { id = resultId }, mejoraCreated);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error al recuperar la mejora creada");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
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
        public async Task<IActionResult> Update(int id, [FromBody]UpdateMejoraCommand command)
        {
            try
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
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
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
            try
            {
                
                await Mediator.Send(new DeleteMejoraByIdCommand { Id = id });

                return NoContent();

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


    }
}
