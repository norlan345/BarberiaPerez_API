using BarberiaPerez_API.Models;
using BarberiaPerez_API.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace BarberiaPerez_API.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IMongoCollection<UsuarioModel> _usuarios;

        public UsuarioService(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _usuarios = database.GetCollection<UsuarioModel>("usuarios");
        }

        public async Task<UsuarioModel> CrearUsuario(UsuarioModel user)
        {
            await _usuarios.InsertOneAsync(user);
            return user;
        }

        public async Task<UsuarioModel?> ObtenerUsuarioAsync(string Nombre) =>
            await _usuarios.Find(u => u.Nombre == Nombre).FirstOrDefaultAsync();

        private string HashPassword(string password)
        {
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
        }

        public async Task<UsuarioModel> Login(UsuarioModel login)
        {
            return await Task.FromResult(login); // Solo para evitar errores, implementa la lógica real
        }

        //public async Task<AgendarCitaModel?> ObtenerCitaPorFechaYHoraAsync(string nombreCliente, DateTime fechaCita)
        //{
        //    return await _citas.Find(c => c.NombreCliente == nombreCliente && c.FechaCita == fechaCita).FirstOrDefaultAsync();
        //}



    }
}
