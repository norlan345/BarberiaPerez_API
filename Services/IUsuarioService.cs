using Amazon.Runtime.Internal;
using BarberiaPerez_API.Models;
using DnsClient;
using System.Threading.Tasks;

namespace BarberiaPerez_API.Services
{
    public interface IUsuarioService
    {
        //Task<UsuarioModel> CrearUsuario(UsuarioModel user);
        Task<UsuarioModel> CrearUsuario(UsuarioModel user);
        Task<LoginModel> Login(LoginModel login);

        Task<UsuarioModel?> ObtenerUsuarioAsync(string Nombre);

        //Task CrearUsuarioAsync(UsuarioModel usuarioModel); // Agrega esta firma si no está
        //Task<UsuarioModel?> ObtenerUsuarioAsync(string nombre);
        //Task<LoginModel> Login(LoginModel login);

    }
}
