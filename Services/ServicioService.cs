using BarberiaPerez_API.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BarberiaPerez_API.Services
{
    public class ServicioService : IServicioService
    {
        private readonly IMongoCollection<ServicioDisponibleModel> _servicios;

        public ServicioService(IMongoDatabase database)
        {
            _servicios = database.GetCollection<ServicioDisponibleModel>("Servicios");
        }

        public async Task<List<ServicioDisponibleModel>> ObtenerServiciosDisponiblesAsync()
        {
            return await _servicios.Find(s => true).ToListAsync();
        }

        public async Task AgregarServiciosBarberoAsync(List<ServicioDisponibleModel> servicios)
        {
            foreach (var servicio in servicios)
            {
                var nuevoServicio = new ServicioDisponibleModel
                {
                    Servicio = servicio.Servicio,
                    Precio = servicio.Precio,
                    Total = servicio.Total,
                };
                await _servicios.InsertOneAsync(nuevoServicio);
            }
        }

        public async Task<ServicioDisponibleModel> ObtenerServicioPorIdAsync(string id)
        {
            return await _servicios.Find(s => s.Id == id).FirstOrDefaultAsync();
        }

        public async Task ActualizarServicioAsync(string id, ServicioDisponibleModel servicioActualizado)
        {
            await _servicios.ReplaceOneAsync(s => s.Id == id, servicioActualizado);
        }

        public async Task EliminarServicioAsync(string id)
        {
            await _servicios.DeleteOneAsync(s => s.Id == id);
        }
    }
}
