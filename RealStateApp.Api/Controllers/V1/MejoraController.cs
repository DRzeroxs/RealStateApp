using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Dto.Mejora;
using RealStateApp.Core.Application.Features.Mejoras.Commands.CreateMejora;
using RealStateApp.Core.Application.Features.Mejoras.Commands.DeleteMejoraById;
using RealStateApp.Core.Application.Features.Mejoras.Commands.UpdateMejora;
using RealStateApp.Core.Application.Features.Mejoras.Queries.GetAllMejoras;
using RealStateApp.Core.Application.Features.Mejoras.Queries.GetMejoraById;

namespace RealStateApp.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Authorize(Roles = "Admin, Developer")]
    public class MejoraController : BaseApiController
    {

        [Authorize(Roles = "Admin, Developer")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MejoraDto))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<MejoraDto>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        [ProducesResponseType(typeof(MejoraDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(CreateMejoraCommand command)
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, UpdateMejoraCommand command)
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
