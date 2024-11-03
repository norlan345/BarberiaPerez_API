using BarberiaPerez_API.Models;
using BarberiaPerez_API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace BarberiaPerez_API.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CitasController : ControllerBase
    {
        private readonly ICitaService _citaService;

        public CitasController(ICitaService citaService)
        {
            _citaService = citaService;
        }

        // POST: api/citas/agendar_cita
        [HttpPost("agendar_cita")]
        public async Task<IActionResult> AgendarCita([FromBody] AgendarCitaModel cita)
        {
            if (cita == null)
                return BadRequest();

            await _citaService.AgregarCitaAsync(cita);
            return Ok("Cita agendada con éxito");
        }

        // GET: api/citas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AgendarCitaModel>>> ObtenerCitas()
        {
            var citas = await _citaService.ObtenerCitasAsync();
            return Ok(citas);
        }

        // GET: api/citas/{id}
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<AgendarCitaModel>> ObtenerCitaPorId(string id)
        {
            var cita = await _citaService.ObtenerCitaPorIdAsync(id);

            if (cita == null)
            {
                return NotFound("Cita no encontrada");
            }

            return Ok(cita);
        }

        // PUT: api/citas/{id}
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> ActualizarCita(string id, [FromBody] AgendarCitaModel citaActualizada)
        {
            if (citaActualizada == null)
            {
                return BadRequest("El modelo de cita no puede ser nulo.");
            }

            var citaExistente = await _citaService.ObtenerCitaPorIdAsync(id);

            if (citaExistente == null)
            {
                return NotFound("Cita no encontrada");
            }

            citaActualizada.Id = id;
            await _citaService.ActualizarCitaAsync(id, citaActualizada);
            return NoContent();
        }

        // DELETE: api/citas/{id}
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> EliminarCita(string id)
        {
            var citaExistente = await _citaService.ObtenerCitaPorIdAsync(id);

            if (citaExistente == null)
            {
                return NotFound("Cita no encontrada");
            }

            await _citaService.EliminarCitaAsync(id);
            return NoContent();
        }




    }
}
