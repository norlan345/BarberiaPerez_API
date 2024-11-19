using BarberiaPerez_API.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BarberiaPerez_API.Services
{
    public class CitaService : ICitaService
    {
        private readonly IMongoCollection<AgendarCitaModel> _citas;

        public CitaService(IMongoDatabase database)
        {
            _citas = database.GetCollection<AgendarCitaModel>("citas");
        }

        public async Task AgregarCitaAsync(AgendarCitaModel cita)
        {
            try
            {
                await _citas.InsertOneAsync(cita);
            }
            catch (Exception ex)
            {
                // Aquí podrías registrar el error o manejarlo como desees
                throw new Exception("Error al agregar cita: " + ex.Message);
            }
        }


        public async Task<List<AgendarCitaModel>> ObtenerCitasAsync()
        {
            try
            {
                // Devuelve todas las citas en la colección
                return await _citas.Find(cita => true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener citas: " + ex.Message);
            }
        }

        public async Task<AgendarCitaModel?> ObtenerCitaPorIdAsync(string id)
        {
            try
            {
                // Busca una cita por su ID
                return await _citas.Find(cita => cita.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener cita por ID: " + ex.Message);
            }
        }

        public async Task ActualizarCitaAsync(string id, AgendarCitaModel cita)
        {
            if (cita == null)
            {
                throw new ArgumentNullException(nameof(cita), "La cita no puede ser nula.");
            }

            try
            {
                // Reemplaza una cita existente con la nueva información
                var resultado = await _citas.ReplaceOneAsync(c => c.Id == id, cita);

                if (resultado.MatchedCount == 0)
                {
                    throw new Exception("No se encontró una cita con el ID proporcionado.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar cita: " + ex.Message);
            }
        }

        public async Task EliminarCitaAsync(string id)
        {
            try
            {
                // Convierte el id a ObjectId si es necesario
                var filter = Builders<AgendarCitaModel>.Filter.Eq(c => c.Id, id);
                var resultado = await _citas.DeleteOneAsync(filter);

                if (resultado.DeletedCount == 0)
                {
                    throw new Exception("No se encontró una cita con el ID proporcionado.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar cita: " + ex.Message);
            }
        }



        public async Task<AgendarCitaModel?> ObtenerCitaPorFechaYHoraAsync(string nombreCliente, DateTime fechaCita)
        {
            return await _citas.Find(c => c.NombreCliente == nombreCliente && c.FechaCita == fechaCita).FirstOrDefaultAsync();
        }




    }
}