using BarberiaPerez_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BarberiaPerez_API.Services;

namespace BarberiaPerez_API.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ReporteGananciasMensualesController : ControllerBase
    {
        private readonly AgendarCitaModel _model;
        private readonly ReporteServicio _reporteService;
        public ReporteGananciasMensualesController(ReporteServicio reporteServicio)
        {
            _reporteService = reporteServicio;
        }

        [HttpPost("reportes_GanaciasMensuales")]
        public IActionResult ReporteGananciasMensuales([FromBody] List<AgendarCitaModel> reportes)
        {
            // Lógica para manejar reportes de clientes
            return Ok("Reportes de GananciasMensuales procesados"); // Ejemplo de respuesta
        }
        //[HttpGet("GananciasMensualesProcesados/{year:int}")]
        //public async Task<ActionResult<IEnumerable<AgendarCitaModel>>> ObtenerReporteAnual(int year)
        //{
        //    try
        //    {
        //        var reportesAnuales = await _reporteService.ObtenerReporteAnualAsync(year);
        //        return Ok(reportesAnuales);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Error al obtener el reporte anual: {ex.Message}");
        //    }
        //}

       

    }
}