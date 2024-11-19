using BarberiaPerez_API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BarberiaPerez_API.Services
{
    public interface ICitaService
    {
        Task AgregarCitaAsync(AgendarCitaModel cita);
        Task<List<AgendarCitaModel>> ObtenerCitasAsync(); 
        Task<AgendarCitaModel?> ObtenerCitaPorIdAsync(string id); 
        Task ActualizarCitaAsync(string id, AgendarCitaModel cita); 
        Task EliminarCitaAsync(string id); 
                                          
        Task<AgendarCitaModel?> ObtenerCitaPorFechaYHoraAsync(string nombreCliente, DateTime fechaCita);

        Task<List<AgendarCitaModel>> obtenerReporteporfechas(DateTime fechacita);

    }
}
