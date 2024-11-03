using BarberiaPerez_API.Models;

namespace BarberiaPerez_API.Services
{
    public interface IServicioService
    {
        Task<List<ServicioDisponibleModel>> ObtenerServiciosDisponiblesAsync();
        Task AgregarServiciosBarberoAsync(List<ServicioDisponibleModel> servicios);
        Task<ServicioDisponibleModel> ObtenerServicioPorIdAsync(string id);
        Task ActualizarServicioAsync(string id, ServicioDisponibleModel servicioActualizado);
        Task EliminarServicioAsync(string id);
    }
}
