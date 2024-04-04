using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Dto.TipoDePropiedad;
using RealStateApp.Core.Application.Dto.TipoVenta;
using RealStateApp.Core.Application.Features.TipoPropiedades.Commands.CreateTipoPropiedad;
using RealStateApp.Core.Application.Features.TipoPropiedades.Commands.UpdateTipoPropiedad;
using RealStateApp.Core.Application.Features.TipoPropiedades.Queries.GetAllTiposPropiedad;
using RealStateApp.Core.Application.Features.TipoPropiedades.Queries.GetTipoPropiedadById;
using RealStateApp.Core.Application.Features.TipoVentas.Commands.CreateTipoVenta;
using RealStateApp.Core.Application.Features.TipoVentas.Commands.DeleteTipoVenta;
using RealStateApp.Core.Application.Features.TipoVentas.Commands.UpdateTipoVenta;
using RealStateApp.Core.Application.Features.TipoVentas.Queries.GetAllTiposVenta;
using RealStateApp.Core.Application.Features.TipoVentas.Queries.GetTipoVentaById;
using RealStateApp.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace RealStateApp.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [SwaggerTag("Mantenimiento de Tipos de ventas")]

    public class TipoVentaController : BaseApiController
    {
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Creacion de un tipo de venta", Description = "Recibe los parametros necesarios para crear un tipo de venta")]

        public async Task<IActionResult> Create([FromBody] CreateTipoVentaCommand command)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Response<int> result = await Mediator.Send(command);
            int createdId = result.Data;
            Response<TipoVentaDto> tipoVentaCreated = await Mediator.Send(new GetTipoVentaByIdQuery { Id = createdId });

            if (tipoVentaCreated != null)
            {
                return CreatedAtAction(nameof(GetById), new { id = createdId }, tipoVentaCreated);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al recuperar el tipo de venta");
            }

        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Actualizacion de un tipo de venta", Description = "Recibe los parametros necesarios para actualizar un tipo de venta existente")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateTipoVentaCommand command)
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
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Listado de tipos de ventas", Description = "Obtiene un listado de todos los tipos de ventas")]
        public async Task<IActionResult> List()
        {
            
            return Ok(await Mediator.Send(new GetAllTiposVentaQuery()));
            
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Developer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Tipo de venta por id", Description = "Obtiene un tipo de venta por el id del mismo")]
        public async Task<IActionResult> GetById(int id)
        {
           
            return Ok(await Mediator.Send(new GetTipoVentaByIdQuery { Id = id }));
            
        }

        [HttpDelete("{id}/Delete")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Eliminar un tipo de venta", Description = "Recibe los parametros necesarios para eliminar un tipo de venta existente")]
        public async Task<IActionResult> Delete(int id)
        {
            
            await Mediator.Send(new DeleteTipoVentaByIdCommand { Id = id });
            return NoContent();
            
        }
    }

}
