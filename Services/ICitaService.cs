using BarberiaPerez_API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BarberiaPerez_API.Services
{
    public interface ICitaService
    {
        Task AgregarCitaAsync(AgendarCitaModel cita); // Método para agendar una nueva cita
        Task<List<AgendarCitaModel>> ObtenerCitasAsync(); // Método para obtener todas las citas
        Task<AgendarCitaModel?> ObtenerCitaPorIdAsync(string id); // Método para obtener una cita por ID
        Task ActualizarCitaAsync(string id, AgendarCitaModel cita); // Método para actualizar una cita
        Task EliminarCitaAsync(string id); // Método para eliminar una cita por
                                           // 
        Task<AgendarCitaModel?> ObtenerCitaPorFechaYHoraAsync(string nombreCliente, DateTime fechaCita);
    }
}
