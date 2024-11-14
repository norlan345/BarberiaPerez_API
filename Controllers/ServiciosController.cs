using BarberiaPerez_API.Models;
using BarberiaPerez_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BarberiaPerez_API.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ServiciosController : ControllerBase
    {
        private readonly IServicioService _servicioService;

        public ServiciosController(IServicioService servicioService)
        {
            _servicioService = servicioService;
        }

        // GET: api/servicios/servicios_disponibles
        [HttpGet("servicios_disponibles")]
        public async Task<IActionResult> GetServiciosDisponibles()
        {
            var servicios = await _servicioService.ObtenerServiciosDisponiblesAsync();
            return Ok(servicios);
        }

        //[HttpPost("servicios_barbero")]
        //public async Task<IActionResult> ServiciosBarbero([FromBody] List<ServicioDisponibleModel> servicios)
        //{
        //    if (servicios == null || servicios.Count == 0)
        //        return BadRequest("La lista de servicios no puede estar vacía.");

        //    // Validar si los campos dentro de cada servicio son válidos
        //    foreach (var servicio in servicios)
        //    {
        //        if (string.IsNullOrEmpty(servicio.Servicio))
        //            return BadRequest("El campo 'Servicio' es obligatorio.");
        //        if (servicio.Precio <= 0)
        //            return BadRequest("El campo 'Precio' debe ser mayor que cero.");
        //        if (servicio.Total <= 0)
        //            return BadRequest("El campo 'Total' debe ser mayor que cero.");
        //    }

        //    await _servicioService.AgregarServiciosBarberoAsync(servicios);
        //    return Ok("Servicios de barbero agregados con éxito");
        //}

        //  POST: api/citas/agendar_cita
        [HttpPost("servicios_barbero")]
        public async Task<IActionResult> ServiciosBarbero([FromBody] ServicioDisponibleModel servicio)
        {
            if (servicio == null)
                return BadRequest("El servicio es requerido.");

            await _servicioService.AgregarServiciosBarberoAsync(servicio);
            return Ok("Servicio agregado con éxito.");
        }



        //// POST: api/servicios/servicios_barbero
        //[HttpPost("servicios_barbero")]
        //public async Task<IActionResult> ServiciosBarbero([FromBody] List<ServicioDisponibleModel> servicios)
        //{
        //    if (servicios == null || servicios.Count == 0)
        //        return BadRequest("La lista de servicios no puede estar vacía.");

        //    await _servicioService.AgregarServiciosBarberoAsync(servicios);
        //    return Ok("Servicios de barbero agregados con éxito");
        //}

        // GET: api/servicios/{id}
        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult> ObtenerServicioPorId(string id)
        {
            var servicio = await _servicioService.ObtenerServicioPorIdAsync(id);
            if (servicio == null)
                return NotFound("Servicio no encontrado");

            return Ok(servicio);
        }

        // PUT: api/servicios/{id}
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> ActualizarServicio(string id, [FromBody] ServicioDisponibleModel servicioActualizado)
        {
            if (servicioActualizado == null)
                return BadRequest("El modelo de servicio no puede ser nulo.");

            var servicioExistente = await _servicioService.ObtenerServicioPorIdAsync(id);
            if (servicioExistente == null)
                return NotFound("Servicio no encontrado");

            servicioActualizado.Id = id;
            await _servicioService.ActualizarServicioAsync(id, servicioActualizado);
            return NoContent();
        }

        // DELETE: api/servicios/{id}
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> EliminarServicio(string id)
        {
            var servicioExistente = await _servicioService.ObtenerServicioPorIdAsync(id);
            if (servicioExistente == null)
                return NotFound("Servicio no encontrado");

            await _servicioService.EliminarServicioAsync(id);
            return NoContent();
        }
    }
}
