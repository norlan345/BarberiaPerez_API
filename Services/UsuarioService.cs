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


        public UsuarioService(MongoDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _usuarios = database.GetCollection<UsuarioModel>("usuarios");
        }

        //public async Task<UsuarioModel> CrearUsuario(UsuarioModel user)
        //{
        //    // Verificar si las contraseñas coinciden


        //    // Crear el nuevo usuario con los datos necesarios
        //    var nuevoUsuario = new UsuarioModel
        //    {
        //        Nombre = user.Nombre,
        //        Contraseña = HashPassword(user.Contraseña), // Hashear la contraseña
        //        Rol = "Cliente" // Asignar rol "Cliente" por defecto
        //    };

        //    // Insertar el nuevo usuario en la colección de MongoDB
        //    await _usuarios.InsertOneAsync(nuevoUsuario);

        //    // Retornar el nuevo usuario como un SignUpModel
        //    return new UsuarioModel
        //    {
        //        Id = nuevoUsuario.Id,
        //        Nombre = nuevoUsuario.Nombre,
        //        Contraseña = nuevoUsuario.Contraseña, // Retornar la contraseña hasheada (solo para fines de demostración)
        //        // Mantener la confirmación
        //    };
        //}

        public async Task<UsuarioModel> CrearUsuario(UsuarioModel user)
        {
            await _usuarios.InsertOneAsync(user);
            return user;
        }

        public async Task<UsuarioModel?> ObtenerUsuarioAsync(string Nombre) =>
        await _usuarios.Find(u => u.Nombre == Nombre).FirstOrDefaultAsync();



        //public async Task<UsuarioModel?> ObtenerUsuarioAsync(string username)
        // => await _usuarios.Find(u => u.Nombre == username).FirstOrDefaultAsync();

        ////public async Task CrearUsuarioAsync(UsuarioModel nuevoUsuario)

        ////    => await _usuarios.InsertOneAsync(nuevoUsuario);
        //public async Task CrearUsuarioAsync(UsuarioModel usuarioModel)
        //{
        //    await _usuarios.InsertOneAsync(usuarioModel);
        //}

        //public async Task<List<UsuarioModel>> Get()
        //   => await _usuarios.FindAsync(
        //       new BsonDocument()).Result.ToListAsync();

        //public async Task<UsuarioModel?> ObtenerUsuarioAsync(string Nombre) =>
        //  await _usuarios.Find(u => u.Nombre == Nombre).FirstOrDefaultAsync();

        // Método para hashear la contraseña (puedes reemplazarlo con un mejor algoritmo)
        private string HashPassword(string password)
        {
            // Implementa aquí tu lógica para hashear la contraseña.
            // Esto es solo un ejemplo sencillo, podrías usar algo más robusto como BCrypt.
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
        }

        public async Task<LoginModel> Login(LoginModel login)
        {
            // Implementar la lógica de inicio de sesión aquí
            return await Task.FromResult(login); // Solo para evitar errores, implementa la lógica real
        }
    }
}
