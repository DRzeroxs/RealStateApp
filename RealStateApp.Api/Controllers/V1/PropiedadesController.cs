﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Dto.Propiedades;
using RealStateApp.Core.Application.Features.Propiedades.Queries.GetAllPropiedad;
using RealStateApp.Core.Application.Features.Propiedades.Queries.GetAllPropiedadesByCode;
using RealStateApp.Core.Application.Features.Propiedades.Queries.GetPropiedadById;
using RealStateApp.Core.Application.Interfaces.IServices;
using RealStateApp.Core.Application.ViewModel.Propiedad;

namespace RealStateApp.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Authorize (Roles ="Admin, Developer")]
   // [SwaggerTag("Controlador de Propiedades")]
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
        public async Task<IActionResult> Get()
        {
            try
            {
                var query = new GetAllPropiedadQuery();
                var propiedades = await Mediator.Send(query);

                return Ok(propiedades);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PropiedadesDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var query = new GetPropiedadByIdQuery { Id = id };
                var propiedad = Mediator.Send(query);

                return Ok(propiedad);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpGet("propiedades/{code}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PropiedadesDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetByCode(int code)
        {
            try
            {
                var query = new GetAllPropiedadesByCode { identifier = code };
                var propiedad = Mediator.Send(query);

                return Ok(propiedad);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
    }
}
