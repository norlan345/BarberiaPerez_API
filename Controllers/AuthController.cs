using BarberiaPerez_API.Models;
using BarberiaPerez_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
using Microsoft.AspNetCore.Identity.Data;

namespace BarberiaPerez_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioService _usuarioServicio;
        private readonly IConfiguration _configuration;

        public AuthController(IUsuarioService usuarioServicio, IConfiguration configuration)
        {
            _usuarioServicio = usuarioServicio;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UsuarioModel login)
        {
            // Buscar usuario en la base de datos
            var existingUser = await _usuarioServicio.ObtenerUsuarioAsync(login.Nombre);

            // Verificar si el usuario existe y si la contraseña es correcta
            if (existingUser == null || existingUser.Contraseña != login.Contraseña)
                return Unauthorized();

            // Generar el token JWT
            var token = GenerateJwtToken(existingUser.Nombre);
            return Ok(new { Token = token });
        }


        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] UsuarioModel user)
        {
            // Verificar si el usuario ya existe
            var existingUser = await _usuarioServicio.ObtenerUsuarioAsync(user.Nombre);
            if (existingUser != null)
            {
                return Conflict("El usuario ya existe.");
            }

            // Validar el rol
            var rolesPermitidos = new[] { "Cliente", "Barbero" };
            if (!rolesPermitidos.Contains(user.Rol))
            {
                user.Rol = "Cliente"; // Rol por defecto
            }

            // Crear un nuevo UsuarioModel
            var nuevoUsuario = new UsuarioModel
            {
                Nombre = user.Nombre,
                Contraseña = BCrypt.Net.BCrypt.HashPassword(user.Contraseña), // Encriptar contraseña
                Rol = user.Rol
            };

            // Guardar en la base de datos
            await _usuarioServicio.CrearUsuario(nuevoUsuario);

            return Ok("Usuario registrado exitosamente.");
        }



     



        // Método privado para generar el token JWT
        private string GenerateJwtToken(string username)
        {
            // Obtener configuración JWT desde appsettings.json
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Crear el token con los claims y credenciales
            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );

            // Devolver el token JWT generado
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
