using BarberiaPerez_API.Models;
using MongoDB.Bson;
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
            return await _servicios.FindAsync(new BsonDocument()).Result.ToListAsync();
        }



        public async Task AgregarServiciosBarberoAsync(ServicioDisponibleModel servicios)
        {
            try
            {
                await _servicios.InsertOneAsync(servicios); 
            }
            catch (Exception ex)
            {
                // Aquí podrías registrar el error o manejarlo como desees
                throw new Exception("Error al agregar cita: " + ex.Message);
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
