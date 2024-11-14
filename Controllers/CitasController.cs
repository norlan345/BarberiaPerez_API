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
        private readonly IServicioService _servicioService;
        public CitasController(ICitaService citaService, IServicioService servicioService)
        {
            _citaService = citaService;
            _servicioService = servicioService;
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

        //[HttpPost]
        //public async Task<IActionResult> AgendarCita([FromBody] AgendarCitaModel cita)
        //{
        //    if (cita == null)
        //    {
        //        return BadRequest("La información de la cita es nula.");
        //    }


        //    await _citaService.AgregarCitaAsync(cita);


        //    var servicio = new ServicioDisponibleModel
        //    {

        //        Servicio = cita.Servicio,

        //        Total = cita.Total,
        //    };

        //    var servicios = new List<ServicioDisponibleModel> { servicio };

        //    await _servicioService.AgregarServiciosBarberoAsync(servicios);

        //    return Ok("Cita y servicio guardados exitosamente.");
        //}

        //[HttpPost]
        //public async Task<IActionResult> AgendarCita([FromBody] AgendarCitaModel cita)
        //{
        //    if (cita == null)
        //    {
        //        return BadRequest("La información de la cita es nula.");
        //    }

        //    // Guardar la cita
        //    await _citaService.AgregarCitaAsync(cita);

        //    // Crear la lista de servicios a partir de los servicios seleccionados en la cita
        //    var servicios = cita.ServiciosSeleccionados.Select(s => new ServicioDisponibleModel
        //    {
        //        Servicio = s.Servicio,
        //        Precio = s.Precio,
        //        Total = cita.Total // Aquí asumiendo que el total de cada servicio es el mismo que el total de la cita
        //    }).ToList();

        //    // Guardar los servicios asociados al barbero o a la cita
        //    await _servicioService.AgregarServiciosBarberoAsync(servicios);

        //    return Ok("Cita y servicios guardados exitosamente.");
        //}



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
        [HttpDelete("{NombreCliente:length(24)}")]
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

     
        [HttpPatch("editar/{id:length(24)}")]
        public async Task<IActionResult> EditarCita(string id, [FromBody] AgendarCitaModel citaEditada)
        {
            if (citaEditada == null)
            {
                return BadRequest("El modelo de cita no puede ser nulo.");
            }


            var citaExistente = await _citaService.ObtenerCitaPorIdAsync(id);
            if (citaExistente == null)
            {
                return NotFound("Cita no encontrada");
            }


            if (!string.IsNullOrEmpty(citaEditada.Servicio))
            {
                citaExistente.Servicio = citaEditada.Servicio;
            }

            if (citaEditada.FechaCita != default)
            {
                citaExistente.FechaCita = citaEditada.FechaCita;
            }

            if (citaEditada.Total != default)
            {
                citaExistente.Total = citaEditada.Total;
            }

            await _citaService.ActualizarCitaAsync(id, citaExistente);
            return Ok("Cita editada con éxito");
        }





    }
}
