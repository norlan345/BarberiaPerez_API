using BarberiaPerez_API.Models;
using MongoDB.Driver;

namespace BarberiaPerez_API.Services
{
    public class ReporteServicio
    {
        private readonly IMongoCollection<AgendarCitaModel> _citasCollection;

        public ReporteServicio(IMongoDatabase database)
        {
            _citasCollection = database.GetCollection<AgendarCitaModel>("Citas");
        }

        public async Task<List<AgendarCitaModel>> ObtenerReporteAnualAsync(int year)
        {
            // Define el rango de fechas para el año
            var inicioDelAño = new DateTime(year, 1, 1);
            var finDelAño = new DateTime(year, 12, 31, 23, 59, 59);

            // Filtro para las citas dentro del año
            var filtro = Builders<AgendarCitaModel>.Filter.And(
                Builders<AgendarCitaModel>.Filter.Gte(c => c.FechaCita, inicioDelAño),
                Builders<AgendarCitaModel>.Filter.Lte(c => c.FechaCita, finDelAño)
            );

            // Consulta a la base de datos
            var reportes = await _citasCollection.Find(filtro).ToListAsync();
            return reportes;
        }


    }
}
