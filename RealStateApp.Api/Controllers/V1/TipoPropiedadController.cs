using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Dto.Mejora;
using RealStateApp.Core.Application.Dto.TipoDePropiedad;
using RealStateApp.Core.Application.Features.Mejoras.Queries.GetMejoraById;
using RealStateApp.Core.Application.Features.TipoPropiedades.Commands.CreateTipoPropiedad;
using RealStateApp.Core.Application.Features.TipoPropiedades.Commands.DeleteTipoPropiedadById;
using RealStateApp.Core.Application.Features.TipoPropiedades.Commands.UpdateTipoPropiedad;
using RealStateApp.Core.Application.Features.TipoPropiedades.Queries.GetAllTiposPropiedad;
using RealStateApp.Core.Application.Features.TipoPropiedades.Queries.GetTipoPropiedadById;
using RealStateApp.Core.Application.Features.TipoVentas.Commands.DeleteTipoVenta;
using RealStateApp.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace RealStateApp.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [SwaggerTag("Mantenimiento de Tipos de Propiedades")]
    public class TipoPropiedadController : BaseApiController
    {
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Creacion de un tipo de propiedad", Description = "Recibe los parametros necesarios para crear un tipo de propiedad")]

        public async Task<IActionResult> Create([FromBody] CreateTipoPropiedadCommand command)
        {
          
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

            Response<int> result = await Mediator.Send(command);
            int createdId = result.Data;
            Response<TipoPropiedadDto> tipoPropiedadCreated = await Mediator.Send(new GetTipoPropiedadByIdQuery { Id = createdId });

            if (tipoPropiedadCreated != null)
            {
                return CreatedAtAction(nameof(GetById), new { id = createdId }, tipoPropiedadCreated);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al recuperar el tipo de propiedad");
            }

        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Actualizacion de un tipo de propiedad", Description = "Recibe los parametros necesarios para actualizar un tipo de propiedad existente")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateTipoPropiedadCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (id != command.Id)
            {
                return BadRequest();
            }

            return Ok(await Mediator.Send(command));
        }
        [HttpGet]
        [Authorize(Roles = "Admin, Developer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Listado de tipos de propiedades", Description = "Obtiene un listado de todos los tipos de propiedades")]

        public async Task<IActionResult> List()
        {
           
            return Ok(await Mediator.Send(new GetAllTiposPropiedadQuery()));
            
        }
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Developer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Tipo de propiedad por id", Description = "Obtiene un tipo de propiedad por el id del mismo")]

        public async Task<IActionResult> GetById(int id)
        {
            
            
            return Ok(await Mediator.Send(new GetTipoPropiedadByIdQuery { Id = id}));
            
        }
        [HttpDelete("{id}/Delete")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Eliminar un tipo de propiedad", Description = "Recibe los parametros necesarios para eliminar un tipo de propiedad existente")]

        public async Task<IActionResult> Delete(int id)
        {
            
            await Mediator.Send(new DeleteTipoPropiedadByIdCommand { Id = id });
            return NoContent();
            
        }
    }
}
