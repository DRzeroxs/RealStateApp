using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Dto.Propiedades;
using RealStateApp.Core.Application.Features.Propiedades.Queries.GetAllPropiedad;
using RealStateApp.Core.Application.Features.Propiedades.Queries.GetAllPropiedadesByCode;
using RealStateApp.Core.Application.Features.Propiedades.Queries.GetPropiedadById;
using RealStateApp.Core.Application.Interfaces.IServices;
using RealStateApp.Core.Application.ViewModel.Propiedad;
using Swashbuckle.AspNetCore.Annotations;

namespace RealStateApp.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Authorize (Roles ="Admin, Developer")]
    [SwaggerTag("Mantenimiento de Propiedades")]
    public class PropiedadesController : BaseApiController
    {
        private readonly IPropiedadService _propiedadService;

        public PropiedadesController(IPropiedadService propiedadService)
        {
            _propiedadService = propiedadService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PropiedadesDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [SwaggerOperation(Summary = "Listado de propiedades", Description = "Obtiene un listado de todas las propiedades")]
        public async Task<IActionResult> Get()
        {
                var query = new GetAllPropiedadQuery();
                var propiedades = await Mediator.Send(query);

                return Ok(propiedades);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PropiedadesDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [SwaggerOperation(Summary = "Propiedad por id", Description = "Obtiene una propiedad por el id del mismo")]
        public async Task<IActionResult> GetById(int id)
        {
                var query = new GetPropiedadByIdQuery { Id = id };
                var propiedad = Mediator.Send(query);

                return Ok(propiedad);
        }

        [HttpGet("propiedades/{code}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PropiedadesDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [SwaggerOperation(Summary = "Propiedad por codigo", Description = "Obtiene una propiedad por el codigo del mismo")]
        public async Task<IActionResult> GetByCode(int code)
        {
                var query = new GetAllPropiedadesByCodeQuery { identifier = code };
                var propiedad = Mediator.Send(query);

                return Ok(propiedad);
        }
    }
}
