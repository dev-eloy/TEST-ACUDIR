using Microsoft.AspNetCore.Mvc;
using Acudir.Test.Domain.Interfaces;
using Acudir.Test.Domain.Models;
using Acudir.Test.Domain.Models.DTOs;

namespace Acudir.Test.Apis.Controllers
{
    /// <summary>
    /// Controlador para gestionar las operaciones de Personas
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class PersonaController : ControllerBase
    {
        #region Fields
        private readonly IPersonaService _personaService;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor del controlador de Personas
        /// </summary>
        /// <param name="personaService">Servicio de personas inyectado</param>
        public PersonaController(IPersonaService personaService)
        {
            _personaService = personaService;
        }
        #endregion

        #region GET Endpoints
        /// <summary>
        /// Obtiene todas las personas con filtros opcionales
        /// </summary>
        /// <param name="nombre">Filtro por nombre completo (opcional)</param>
        /// <param name="edad">Filtro por edad exacta (opcional)</param>
        /// <param name="domicilio">Filtro por domicilio (opcional)</param>
        /// <param name="telefono">Filtro por teléfono (opcional)</param>
        /// <param name="profesion">Filtro por profesión (opcional)</param>
        /// <returns>Lista de personas que coinciden con los filtros</returns>
        /// <response code="200">Lista de personas obtenida exitosamente</response>
        /// <response code="500">Error interno del servidor</response>
        [HttpGet("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<Persona>>> GetAll(
            [FromQuery] string? nombre = null,
            [FromQuery] int? edad = null,
            [FromQuery] string? domicilio = null,
            [FromQuery] string? telefono = null,
            [FromQuery] string? profesion = null)
        {
            try
            {
                var personas = await _personaService.GetAllAsync(nombre, edad, domicilio, telefono, profesion);
                return Ok(personas);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error interno del servidor al obtener las personas");
            }
        }
        #endregion

        #region POST Endpoints
        /// <summary>
        /// Agrega una nueva persona al sistema
        /// </summary>
        /// <param name="persona">Datos de la persona a agregar</param>
        /// <returns>Resultado de la operación</returns>
        /// <response code="201">Persona creada exitosamente</response>
        /// <response code="400">Datos de entrada inválidos o persona ya existe</response>
        /// <response code="500">Error interno del servidor</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Post([FromBody] Persona persona)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _personaService.AddAsync(persona);

                if (!result.Success)
                {
                    return BadRequest(new { Errors = result.Errors, Message = result.Message });
                }

                return CreatedAtAction(nameof(GetAll), persona);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error interno del servidor al crear la persona");
            }
        }
        #endregion

        #region PUT Endpoints
        /// <summary>
        /// Actualiza una persona existente en el sistema
        /// </summary>
        /// <param name="persona">Datos actualizados de la persona</param>
        /// <returns>Resultado de la operación</returns>
        /// <response code="204">Persona actualizada exitosamente</response>
        /// <response code="400">Datos de entrada inválidos</response>
        /// <response code="404">Persona no encontrada</response>
        /// <response code="500">Error interno del servidor</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Put([FromBody] Persona persona)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _personaService.UpdateAsync(persona);

                if (!result.Success)
                {
                    if (result.Errors.Any(e => e.Contains("No se encontró")))
                    {
                        return NotFound(new { Errors = result.Errors, Message = result.Message });
                    }
                    return BadRequest(new { Errors = result.Errors, Message = result.Message });
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error interno del servidor al actualizar la persona");
            }
        }
        #endregion
    }
}